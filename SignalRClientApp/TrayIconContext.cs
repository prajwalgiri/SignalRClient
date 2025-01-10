using System;
using System.Collections.Generic;
using System.ComponentModel.Design;
using System.Linq;
using System.Resources;
using System.Text;
using System.Threading.Tasks;

namespace SignalRClientApp;

internal class TrayIconContext:ApplicationContext
{
    ClientApp clientApp;
    private NotifyIcon trayIcon;
    public TrayIconContext()
    {

        // Initialize Tray Icon
        trayIcon = new NotifyIcon()
        {
            
         
            ContextMenuStrip =CreateContextMenu(),
            Visible = true,
            Icon = new Icon("Resources/LifeSafer.ico"),
            Text="PlusClientLiteWorker",
            
        };
        trayIcon.DoubleClick += Open;
    }
    private ContextMenuStrip CreateContextMenu()
    {
        // Add menu items to context menu
        var contextMenu = new ContextMenuStrip();
        contextMenu.Items.Add("Exit", null, Exit);
        contextMenu.Items.Add("Show/Hide", null, Open);
        return contextMenu;
    }
    void Exit(object? sender, EventArgs e)
    {
        // Hide tray icon, otherwise it will remain shown until user mouses over it
        trayIcon.Visible = false;

        Application.Exit();
    }
    void Open(object? sender, EventArgs e)
    {
        // Hide tray icon, otherwise it will remain shown until user mouses over it
        //Application.Run(new ClientApp());
        if(clientApp==null)clientApp = new ClientApp();
        clientApp.Show();
    }

}