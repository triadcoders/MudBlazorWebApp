namespace MudBlazorWebApp.Service;
using MudBlazorWebApp.Client;

public class MessageController : IMessageController
{
    public  string AnnounceMessage()
    {
        return "Hello From the Server";
    }
}