// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.
#nullable disable

using System;
using System.ComponentModel.DataAnnotations;
using System.Text;
using System.Text.Encodings.Web;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.WebUtilities;
using Microsoft.EntityFrameworkCore;
using RecipeApp.Models;

namespace RecipeApp.Areas.Identity.Pages.Account.Manage
{
    public class PhotoModel : PageModel
    {
        private readonly UserManager<IdentityUser> _userManager;
        private readonly SignInManager<IdentityUser> _signInManager;
        private readonly IEmailSender _emailSender;
        private readonly RecipeAppContext _context;

        [BindProperty]
        public BufferedSingleFileUploadDb? FileUpload { get; set; }

        public byte[] Picture { get; set; }

        public UserDetail UserDetails { get; set; }

        [TempData]
        public string StatusMessage { get; set; }

        public PhotoModel(
            UserManager<IdentityUser> userManager,
            SignInManager<IdentityUser> signInManager,
            IEmailSender emailSender,
            RecipeAppContext context)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _emailSender = emailSender;
            _context = context;
        }

        private async Task LoadAsync(IdentityUser user)
        {
            var UserDetails = _context.UserDetails.Where(p => p.UserId == user.Id).FirstOrDefault();
            if (UserDetails != null)
            {
                Picture = UserDetails.Photo;
            }
            else
            {
                //Read Image File into Image object.
                string path = "./wwwroot/images/profile1.png";
                var memoryStream = new MemoryStream();
                using (var stream = System.IO.File.OpenRead(path))
                {
                    await new FormFile(stream, 0, stream.Length, null, Path.GetFileName(stream.Name)).CopyToAsync(memoryStream);
                    Picture = memoryStream.ToArray();
                };
            }
        }

        public async Task<IActionResult> OnGetAsync()
        {
            var user = await _userManager.GetUserAsync(User);
            if (user == null)
            {
                return NotFound($"Unable to load user with ID '{_userManager.GetUserId(User)}'.");
            }

            await LoadAsync(user);
            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            var user = await _userManager.GetUserAsync(User);
            if (user == null)
            {
                return NotFound($"Unable to load user with ID '{_userManager.GetUserId(User)}'.");
            }
            if (!ModelState.IsValid)
            {
                await LoadAsync(user);
                return Page();
            }
            UserDetails = _context.UserDetails.Where(p => p.UserId == user.Id).FirstOrDefault();
            if (UserDetails.Photo != Picture)
            {
                var memoryStream = new MemoryStream();
                await FileUpload.FormFile.CopyToAsync(memoryStream);
                UserDetails.Photo = memoryStream.ToArray();
                UserDetails.UserId = user.Id;

                _context.UserDetails.Update(UserDetails);
                await _context.SaveChangesAsync();
                StatusMessage = "Your profile photo is changed.";
                return RedirectToPage();
            }
            return RedirectToPage();
        }

    }
}
