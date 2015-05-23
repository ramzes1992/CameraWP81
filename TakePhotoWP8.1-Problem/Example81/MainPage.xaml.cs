using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Threading.Tasks;
using Windows.Devices.Enumeration;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.Media;
using Windows.Media.Capture;
using Windows.Media.MediaProperties;
using Windows.Storage;
using Windows.Storage.Streams;
using Windows.UI.Popups;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media.Imaging;
using Windows.UI.Xaml.Navigation;
using System.Net;//.Networking.Sockets;
using System.Threading;
using System.Text;


namespace Example81
{
    public sealed partial class MainPage : Page
    {
        Windows.Media.Capture.MediaCapture captureManager;

        public MainPage()
        {
            this.InitializeComponent();
        }

        public WriteableBitmap _bitmap { get; set; }

        bool isCapturing = false;
        bool isFirst = true;
        private async void v_Button_Capture_Click(object sender, RoutedEventArgs e)
        {
            isCapturing = !isCapturing;

            if (isCapturing)
            {
                if (isFirst)
                {
                    captureManager = new MediaCapture();

                    await captureManager.InitializeAsync();
                    capturePreview.Source = captureManager;
                    await captureManager.StartPreviewAsync();
                    isFirst = false;
                }
                v_Image.Source = null;
            }
            else
            {
                ImageEncodingProperties imageProperties = ImageEncodingProperties.CreateJpeg();

                var stream = new InMemoryRandomAccessStream();

                await captureManager.CapturePhotoToStreamAsync(imageProperties, stream);

                _bitmap = new WriteableBitmap(300, 300);
                stream.Seek(0);
                await _bitmap.SetSourceAsync(stream);

                //await captureManager.StopPreviewAsync();

                v_Image.Source = _bitmap;
            }
        }
    }
}
