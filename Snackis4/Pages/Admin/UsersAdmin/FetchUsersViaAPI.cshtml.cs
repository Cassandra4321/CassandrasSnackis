using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Snackis4.Models;
using System.Text.Json;

namespace Snackis4.Pages.Admin.UsersAdmin
{
    [Authorize(Roles = "Admin")]
    public class FetchUsersViaAPIModel : PageModel
    {
        private readonly HttpClient _httpClient;

        public FetchUsersViaAPIModel(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public List<User> Users { get; set; } = new List<User>();
        public string ErrorMessage { get; set; }


        public async Task OnGetAsync()
        {
            Users = await GetUsersAsync();
        }

        private async Task<List<User>> GetUsersAsync()
        {
            var response = await _httpClient.GetAsync("https://cassandrassnackisapi.azurewebsites.net/api/User");
            if (response.IsSuccessStatusCode)
            {
                return await response.Content.ReadFromJsonAsync<List<User>>() ?? new List<User>();
            }
            return new List<User>();
        }

        public async Task<IActionResult> OnGetDeleteAsync(string id)
        {
            try
            {
                var response = await _httpClient.DeleteAsync($"https://cassandrassnackisapi.azurewebsites.net/api/user/{id}");
                if (response.IsSuccessStatusCode)
                {
                    Users = await GetUsersAsync();
                }
                else
                {
                    ErrorMessage = "Failed to delete the user.";
                }
                return RedirectToPage();
            }
            catch (Exception ex)
            {
                ErrorMessage = "Couldn't delete the user: " + ex.Message;
                return Page();
            }
        }

    }
}


