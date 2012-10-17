using System;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Ink;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Shapes;

namespace INdT.Views
{
    public class Util
    {
        public static void requestDone(WaitingView waitingView
                                            , bool[] requests
                                            , int requestIndex
                                            , bool failed
                                            , Action action = null)
        {
            requests[requestIndex] = true;
            Util.checkWaitingViewMessage(waitingView, requests, failed);
            if (action != null)
            {
                Deployment.Current.Dispatcher.BeginInvoke(() => { action(); });
            }
        }

        private static void checkWaitingViewMessage(WaitingView waitingView, bool[] requests, bool anyHasFailed)
        {
            Deployment.Current.Dispatcher.BeginInvoke(() =>
            {
                if (waitingView.Visibility == Visibility.Collapsed)
                {
                    return;
                }

                if (anyHasFailed)
                {
                    waitingView.Message = "Não foi possível carregar todas as informações.";
                    waitingView.close();
                }
                else
                {
                    foreach (bool requestFinished in requests)
                    {
                        if (!requestFinished)
                        {
                            return;
                        }
                    }
                    waitingView.close();
                }
            });             // dispatcher
        }
    }
}
