using Main.Models;
using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Main.Controllers
{
	[Authorize]
	public class HomeController : Controller
	{
		public ActionResult Chats()
		{
			using(ApplicationDbContext db = new ApplicationDbContext())
			{
				var user = db.Users.SingleOrDefault(x => x.UserName == User.Identity.Name);
				var viewModel = user.Chats.ToList();
				for(var i = 0; i < viewModel.Count; i++)
				{
					try
					{
						if (!viewModel[i].IsGroupChat)
						{
							var users = viewModel[i].Users.Select(x => x.UserName).ToList();
							viewModel[i].Title = users[0] == user.UserName ? users[1] : users[0];
						}
					}
					catch {
						viewModel.RemoveAt(i--);
					}
				}
				viewModel = viewModel.Concat(db.Chats.Where(x => x.IsGroupChat))
					.Distinct().OrderBy(x => x.IsGroupChat).ThenBy(x => x.Title).ToList();

				return View(viewModel);
			}
		}

		public ActionResult OpenChat(int? chatID)
		{
			if (!chatID.HasValue) return RedirectToAction("Chats");
			using(ApplicationDbContext db = new ApplicationDbContext())
			{
				var chat = db.Chats.Where(x => x.ID == chatID).SingleOrDefault();
				if(chat == null) return RedirectToAction("Chats");
				if (!chat.IsGroupChat && !chat.Users.Select(x => x.UserName).Contains(User.Identity.Name))
					return RedirectToAction("Chats");

				ViewBag.ChatID = chatID;
				return View(chat.Messages.Select(x => new MessageModel()
				{
					ID = x.ID,
					SenderName = x.Sender.UserName,
					Content = x.Content,
					SendTime = x.SendTime.ToString(@"HH:mm"), 
					SenderImg = x.Sender.ImgSrc
				}).ToList());
			}
		}

		public ActionResult LogOut()
		{
			HttpContext.GetOwinContext().Authentication.SignOut(DefaultAuthenticationTypes.ApplicationCookie);
			return RedirectToAction("Login", "Account");
		}

		public ActionResult SendMsg(int? chatID, string msg)
		{
			msg = msg.Trim(' ');
			if (!chatID.HasValue || msg == null || msg.Length==0) return Json(null);

			using(ApplicationDbContext db = new ApplicationDbContext())
			{
				string myName = User.Identity.Name;
				var chat = db.Chats.Where(x => x.ID == chatID).SingleOrDefault();
				if (chat == null) return Json(null);
				if(chat.IsGroupChat || chat.Users.Select(x => x.UserName).Contains(myName))
				{
					Message message = new Message()
					{
						ChatID = chatID.Value,
						Content = msg,
						SenderID = db.Users.Where(x => x.UserName == myName).SingleOrDefault()?.Id,
						SendTime = DateTime.Now
					};
					db.Messages.Add(message);
					db.SaveChanges();
				}
			}

			return Json(null);
		}


		[AllowAnonymous]
		public ActionResult GetMessages(int? chatID, int? lastMsgID)
		{
			if (!chatID.HasValue) return Json(null);

			using(ApplicationDbContext db = new ApplicationDbContext())
			{
				var messages = db.Chats.Where(x => x.ID == chatID).SingleOrDefault().Messages.OrderByDescending(x => x.SendTime)
					.Select(x => new MessageModel() 
					{
						ID = x.ID,
						SendTime=x.SendTime.ToString(@"HH:mm"),
						Content=x.Content, 
						SenderName=x.Sender.UserName,
						SenderImg=x.Sender.ImgSrc
					}).ToList();

				if (messages.Count == 0 || messages[0].ID == lastMsgID) return Json(new List<Message>());

				object json = null;
				if (lastMsgID.HasValue)
				{
					var result = new List<MessageModel>();
					for (int i = 0; i < messages.Count; i++)
					{
						MessageModel msg = messages[i];
						if (msg.ID == lastMsgID)
						{
							result.Reverse();
							string myName = User.Identity.Name;
							json = result.Select(x => new
							{
								ID = x.ID,
								Content = x.Content,
								Sender = x.SenderName,
								Time = x.SendTime,
								ImgSrc = x.SenderImg,
								IsMyMsg = x.SenderName == myName
							});
							break;
						}
						result.Add(msg);
					}
					return Json(json);
				}
				else
				{
					messages.Reverse();
					string myName = User.Identity.Name;
					json = messages.Select(x => new
					{
						ID = x.ID,
						Content = x.Content,
						Sender = x.SenderName,
						Time = x.SendTime,
						ImgSrc = x.SenderImg,
						IsMyMsg = x.SenderName == myName
					});
				}
				return Json(json);
			}
		}
	
		public ActionResult About()
		{
			return View();
		}
	}
}