using System;
using System.ComponentModel;
using System.Threading.Tasks;
using Windows.Media.Capture;
using Windows.Media.MediaProperties;
using Windows.Storage.Streams;
using Kopigi.Portable.Object;

namespace Kopigi.NetCore.UAP.Media
{
    /// <summary>
    /// Classe permettant l'enregistrement d'un media vocal
    /// </summary>
    public class MediaAudioRecorder : NotifyPropertyChanged, IDisposable
    {
        private MediaCapture _mediaCapture;
        private IRandomAccessStream _audioStream;
        private MediaEncodingProfile _encodingProfile;

        #region properties

        /// <summary>
        /// Indique le statut courant du Recorder
        /// </summary>
        private RecordStatus _recordStatus;
        public RecordStatus RecordStatus
        {
            get { return _recordStatus; }
            set
            {
                _recordStatus = value;
                RaisePropertyChanged();
            }
        }

        #endregion

        /// <summary>
        /// Créé une instance du MediaRecorder
        /// </summary>
        public MediaAudioRecorder(EncodingRecord encoding, AudioEncodingQuality quality)
        {
            switch (encoding)
            {
                case EncodingRecord.Wav:
                    _encodingProfile = MediaEncodingProfile.CreateWav(quality);
                    break;
                case EncodingRecord.MP3:
                    _encodingProfile = MediaEncodingProfile.CreateMp3(quality);
                    break;
                case EncodingRecord.M4a:
                    _encodingProfile = MediaEncodingProfile.CreateM4a(quality);
                    break;
            }
            RecordStatus = RecordStatus.NotStart;
            InitMediaCapture();
        }

        #region public

        public void Dispose()
        {
            _mediaCapture.Dispose();
        }

        /// <summary>
        /// Permet d'enregistrer un media vocal
        /// </summary>
        public async void Record()
        {
            RecordStatus = RecordStatus.Progress;
            _audioStream = new InMemoryRandomAccessStream();
            _mediaCapture.StopRecordAsync();
            await _mediaCapture.StartRecordToStreamAsync(_encodingProfile, _audioStream);
        }

        /// <summary>
        /// Permet de renvoyer les informations d'un media enregistré
        /// </summary>
        public async Task<byte[]> SaveRecord()
        {
            using (var dataReader = new DataReader(_audioStream.GetInputStreamAt(0)))
            {
                await dataReader.LoadAsync((uint)_audioStream.Size);
                var buffer = new byte[(int)_audioStream.Size];
                dataReader.ReadBytes(buffer);

                return buffer;
            }
        }

        /// <summary>
        /// Permet de stopper l'enregistrement d'un media vocal
        /// </summary>
        public async void StopRecording()
        {
            RecordStatus = RecordStatus.Success;
            await _mediaCapture.StopRecordAsync();
        }
        #endregion

        #region private

        private async Task InitMediaCapture()
        {
            _mediaCapture = new MediaCapture();
            var captureInitSettings = new MediaCaptureInitializationSettings
            {
                StreamingCaptureMode = StreamingCaptureMode.Audio
            };
            await _mediaCapture.InitializeAsync(captureInitSettings);
            _mediaCapture.Failed += MediaCaptureOnFailed;
            _mediaCapture.RecordLimitationExceeded += MediaCaptureOnRecordLimitationExceeded;
        }
        #endregion

        #region private

        private async void MediaCaptureOnRecordLimitationExceeded(MediaCapture sender)
        {
            await sender.StopRecordAsync();
            RecordStatus = RecordStatus.Failed;
        }

        private async void MediaCaptureOnFailed(MediaCapture sender, MediaCaptureFailedEventArgs errorEventArgs)
        {
            RecordStatus = RecordStatus.Failed;
        }
        #endregion
    }

    /// <summary>
    /// Statut de l'enregistrement
    /// </summary>
    public enum RecordStatus
    {
        /// <summary>
        /// Enregistrement pas encore demarré
        /// </summary>
        NotStart,

        /// <summary>
        /// Enregistrement en cours
        /// </summary>
        Progress,

        /// <summary>
        /// Enregistrement fait avec succés
        /// </summary>
        Success,

        /// <summary>
        /// Enregistrement comportant des erreurs
        /// </summary>
        Failed
    }

    /// <summary>
    /// Type d'encodage de l'enregistrement
    /// </summary>
    public enum EncodingRecord
    {
        /// <summary>
        /// Format Wav
        /// </summary>
        Wav,

        /// <summary>
        /// Format MP3
        /// </summary>
        MP3,

        /// <summary>
        /// Format M4a
        /// </summary>
        M4a
    }
}
