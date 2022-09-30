namespace ChatApp.Controllers
{
	using Microsoft.AspNetCore.Mvc;

	using ChatApp.Models;
	public class ChatController : Controller
	{
		private static List<KeyValuePair<string, string>> AllMesages = new List<KeyValuePair<string, string>>();

		[HttpGet]
		public IActionResult Show()
		{
			if (AllMesages.Count < 1)
			{
				return this.View(new ChatViewModel());
			}

			ChatViewModel chatModel = new ChatViewModel()
			{
				Messages = AllMesages.Select(m => new MessageViewModel()
				{
					Sender = m.Key,
					MessageText = m.Value
				})
				.ToList()
			};

			return this.View(chatModel);
		}

		[HttpPost]
		public IActionResult Send(ChatViewModel chat)
		{
			MessageViewModel newMessage = chat.CurrentMessage;

			AllMesages.Add(new KeyValuePair<string, string>(newMessage.Sender, newMessage.MessageText));
			
			return this.RedirectToAction("Show");
		}
	}
}
