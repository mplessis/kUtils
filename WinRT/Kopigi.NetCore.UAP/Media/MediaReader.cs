using System;
using System.ComponentModel;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Storage.Streams;
using Windows.UI.Core;
using Windows.UI.Xaml.Controls;
using Kopigi.Portable.Object;

namespace Kopigi.NetCore.UAP.Media
{
    /// <summary>
    /// Classe permettant la lecture d'un media vocal
    /// </summary>
    public class MediaReader : NotifyPropertyChanged
    {
        #region properties

        /// <summary>
        /// Indique si la lecture est en cours
        /// </summary>
        private bool _isReading;
        public bool IsReading
        {
            get { return _isReading; }
            set
            {
                _isReading = value;
                RaisePropertyChanged();
            }
        }
        public MediaElement MediaElem { get; set; }
        
        #endregion

        /// <summary>
        /// Créé une instance pour la lecture d'un media
        /// </summary>
        /// <param name="mediaElement">Objet <see cref="MediaElement"/> permettant la lecture du le client</param>
        public MediaReader(MediaElement mediaElement)
        {
            MediaElem = mediaElement;
        }

        #region public

        /// <summary>
        /// Lance la lecture du media
        /// </summary>
        /// <param name="datas"></param>
        public async void Read(byte[] datas)
        {
            IsReading = true;
            var audioStream = new InMemoryRandomAccessStream();
            await audioStream.WriteAsync(datas.AsBuffer());
            audioStream.Seek(0);
            MediaElem.SetSource(audioStream, "audio/wav");
            MediaElem.MediaEnded += MediaElem_MediaEnded;
            await MediaElem.Dispatcher.RunAsync(CoreDispatcherPriority.Normal, () =>
            {
                MediaElem.Stop();
                MediaElem.Play();
            });
        }

        /// <summary>
        /// Stoppe la lecture du media
        /// </summary>
        public void Stop()
        {
            if (MediaElem != null)
            {
                MediaElem.Stop();
                IsReading = false;
            }
        }

        #endregion

        #region events

        void MediaElem_MediaEnded(object sender, Windows.UI.Xaml.RoutedEventArgs e)
        {
            MediaElem.Stop();
            IsReading = false;
        }

        #endregion
    }
}
