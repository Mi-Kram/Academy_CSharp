using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;

namespace Main.Models
{
  // В профиль пользователя можно добавить дополнительные данные, если указать больше свойств для класса ApplicationUser. Подробности см. на странице https://go.microsoft.com/fwlink/?LinkID=317594.
  public class ApplicationUser : IdentityUser
  {
    [MaxLength(50)]
    public override string Email { get; set; }

    [MaxLength(200)]
		public string ImgSrc { get; set; }


		public virtual ICollection<Chat> Chats { get; set; }

    public async Task<ClaimsIdentity> GenerateUserIdentityAsync(UserManager<ApplicationUser> manager)
    {
      // Обратите внимание, что authenticationType должен совпадать с типом, определенным в CookieAuthenticationOptions.AuthenticationType
      var userIdentity = await manager.CreateIdentityAsync(this, DefaultAuthenticationTypes.ApplicationCookie);
      // Здесь добавьте утверждения пользователя
      return userIdentity;
    }
  }

	public class Chat
	{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int ID { get; set; }

    [MaxLength(15)]
		public string Title { get; set; }

		public bool IsGroupChat { get; set; }
		public virtual ICollection<Message> Messages { get; set; }
		public virtual ICollection<ApplicationUser> Users { get; set; }
	}

  public class Message
	{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
		public int ID { get; set; }

    [MaxLength(200)]
		public string Content { get; set; }

		public DateTime SendTime { get; set; }

    [MaxLength(128)]
		public string SenderID { get; set; }

		public int ChatID { get; set; }

    [ForeignKey("SenderID")]
		public virtual ApplicationUser Sender { get; set; }
    [ForeignKey("ChatID")]
    public virtual Chat Chat { get; set; }

	}

 // public class ChatMessage
	//{
 //   [Key]
 //   public int MessageID { get; set; }

 //   public int ChatID { get; set; }

 //   [ForeignKey("MessageID")]
 //   public virtual Message Message { get; set; }
 //   [ForeignKey("ChatID")]
 //   public virtual Chat Chat { get; set; }
	//}

 // public class ChatUser
	//{
 //   [Key]
	//	public string UserID { get; set; }
 //   [Key]
 //   public int ChatID { get; set; }

 //   [ForeignKey("UserID")]
	//	public ApplicationUser User { get; set; }
 //   [ForeignKey("ChatID")]
 //   public Chat Chat { get; set; }
	//}

  public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
  {
		public virtual DbSet<Chat> Chats { get; set; }
		public virtual DbSet<Message> Messages { get; set; }
		//public virtual DbSet<ChatMessage> ChatMessages { get; set; }

		public ApplicationDbContext()
        : base("DB_Context", throwIfV1Schema: false)
    {
    }

    public static ApplicationDbContext Create()
    {
      return new ApplicationDbContext();
    }
  }
}