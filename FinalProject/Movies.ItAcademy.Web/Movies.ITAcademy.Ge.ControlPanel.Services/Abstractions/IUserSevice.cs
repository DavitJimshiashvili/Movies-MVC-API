using Movies.ITAcademy.Ge.ControlPanel.Services.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Movies.ITAcademy.Ge.ControlPanel.Services.Abstractions
{
    public interface IUserSevice
    {
        //მომხმარებელები თავად დარეგისტრირდებიან სისტემაში და მხოლოდ ადმინს შეეძლება მიანიჭოს რომელიმე მათთაგანს მოდერატორის როლი )
        //    (ადმინს უნდა უჩანდეს მომხმარებლების სია და შეძლოს მათი წაშლა დამატება ან რედაქტირება, რედაქტირებიდან უნდა შეეძლოს კონკრეტული როლის მინიჭება მომხმარებლისთვის.)
        Task<List<UserServiceModel>> GetAllAsync();//get all users
        Task<UserServiceModel> GetAsync(string id);
        Task<string> CreateAsync(UserServiceModel user);//returns created user's ID
        Task UpdateAsync(UserServiceModel user);//returns updated userID
        Task DeleteAsync(string id);
        Task<bool> Exists(string id);//returns true or false
        Task CancelTicketAsync(string userId, int movieId);//sheidzl;eba gadaketebac
        Task<List<TicketServiceModel>> GetUserTickets(string userId);
        Task<List<TicketServiceModel>> GetAllBookedTickets();
    }
}
