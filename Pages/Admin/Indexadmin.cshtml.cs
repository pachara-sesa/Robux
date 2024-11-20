using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Identity;
using AS3_1660706126.Data;
using AS3_1660706126.Areas.Identity.Data;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace AS3_1660706126.Pages.Admin
{
    public class IndexadminModel : PageModel
    {
        private readonly FinalProjectContext _context;
        private readonly UserManager<FinalProjectUser> _userManager;

        public IndexadminModel(FinalProjectContext context, UserManager<FinalProjectUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        // List of users to display on the page
        public List<FinalProjectUser> Users { get; set; }

        // Message to display feedback after actions
        public string Message { get; set; }

        [BindProperty]
        public string PasswordChangeUserId { get; set; }

        [BindProperty]
        public string NewPassword { get; set; }

        [BindProperty]
        public string ConfirmPassword { get; set; }
        [BindProperty]
        public string EmailChangeUserId { get; set; }

        [BindProperty]
        public string NewEmail { get; set; }

        public async Task OnGetAsync()
        {
            // Fetch all users from the database
            Users = await _context.Users.ToListAsync();
        }

        public async Task<IActionResult> OnPostDeleteAsync(string id)
        {
            if (string.IsNullOrEmpty(id))
            {
                // If the ID is null or empty, return bad request
                return BadRequest();
            }

            // Find the user by ID
            var user = await _context.Users.FindAsync(id);
            if (user == null)
            {
                // If user not found, return not found result
                return NotFound();
            }

            // Remove the user from the database
            _context.Users.Remove(user);
            await _context.SaveChangesAsync();

            // Set a success message
            Message = "User deleted successfully!";

            // Refresh the page
            return RedirectToPage();
        }

        public async Task<IActionResult> OnPostChangePasswordAsync()
        {
            if (NewPassword != ConfirmPassword)
            {
                ModelState.AddModelError(string.Empty, "Passwords do not match.");
                return Page();
            }

            var user = await _userManager.FindByIdAsync(PasswordChangeUserId);
            if (user == null)
            {
                ModelState.AddModelError(string.Empty, "User not found.");
                return NotFound();
            }

            var resetToken = await _userManager.GeneratePasswordResetTokenAsync(user);
            var result = await _userManager.ResetPasswordAsync(user, resetToken, NewPassword);

            if (result.Succeeded)
            {
                Message = "Password has been reset successfully!";
                return RedirectToPage();
            }

            foreach (var error in result.Errors)
            {
                ModelState.AddModelError(string.Empty, error.Description);
            }

            return Page();
        }
        public async Task<IActionResult> OnPostChangeEmailAsync()
        {
            if (string.IsNullOrEmpty(NewEmail))
            {
                ModelState.AddModelError(string.Empty, "New email cannot be empty.");
                return Page();
            }

            var user = await _userManager.FindByIdAsync(EmailChangeUserId);
            if (user == null)
            {
                ModelState.AddModelError(string.Empty, "User not found.");
                return NotFound();
            }

            // Update the user's email
            user.Email = NewEmail;
            user.UserName = NewEmail; // Optional: Update username to match the email
            var result = await _userManager.UpdateAsync(user);

            if (result.Succeeded)
            {
                Message = "Email has been updated successfully!";
                return RedirectToPage();
            }

            foreach (var error in result.Errors)
            {
                ModelState.AddModelError(string.Empty, error.Description);
            }

            return Page();
        }

    }
}
