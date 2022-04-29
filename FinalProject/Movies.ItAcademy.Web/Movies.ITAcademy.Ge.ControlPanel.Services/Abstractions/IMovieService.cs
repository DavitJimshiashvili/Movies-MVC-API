using Movies.ITAcademy.Ge.ControlPanel.Services.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Movies.ITAcademy.Ge.ControlPanel.Services.Abstractions
{
    public interface IMovieService
    {
        //damateba moderatori amatebs da statusi eqneba uploaded sanam admin ar etyvis dapublishebas
        //washla
        //daredaqtireba
        //get all uploaded
        Task<List<MovieServiceModel>> GetAllAsync();
        Task<List<MovieServiceModel>> GetAllPendingAsync();
        Task<MovieServiceModel> GetAsync(int id);
        Task<MovieServiceModel> GetNoTrackingAsync(int id);
        Task<int> CreateAsync(MovieServiceModel movie);
        Task<int> UpdateAsync(MovieServiceModel movie);
        Task DeleteAsync(int id);//deletes with movieID
        Task<bool> Exists(int id);//returns true or false

    }
}
