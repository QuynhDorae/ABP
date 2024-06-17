using Microsoft.AspNetCore.SignalR;
using ProductApp.Hubs;
using System.Threading.Tasks;

namespace ProductApp
{
    public class DocumentService
    {
        private readonly IHubContext<DocumentHub> _documentHub;
        public DocumentService(IHubContext<DocumentHub> documentHub)
        {
            _documentHub = documentHub;
        }
        public async Task NotifyDocumentChanged(int documentId, string changeType)
        {
            await _documentHub.Clients.All.SendAsync("DocumentChanged", documentId, changeType);
        }
    }
}
