using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LifeManagementApp.Models;

namespace LifeManagementApp.Interfaces
{
    public interface IJokeService
    {
        Task<List<Joke>> GetJokesAsync();
    }
}
