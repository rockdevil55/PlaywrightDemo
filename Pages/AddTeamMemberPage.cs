using Microsoft.Playwright;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PlaywrightDemo.pages
{
    public class AddTeamMemberPage
    {
        private readonly IPage _page;

        private readonly ILocator _firstNameTxt;
        private readonly ILocator _lastNameTxt;
        private readonly ILocator _emailTxt;
        private readonly ILocator _phoneTxt;
        private readonly ILocator _userNameTxt;
        private readonly ILocator _passwordTxt;
        private readonly ILocator _confirmPasswordTxt;
        private readonly ILocator _pageTitle;
        private readonly ILocator _saveAndCloseBtn;
        private readonly ILocator _savedMessageToastr;
        private readonly ILocator _studentsRecordList;
        private readonly ILocator _deleteBtn;
        private readonly ILocator _confirmPopupDeleteBtn;
        private readonly ILocator _confirmPopupMessage;
        private readonly ILocator _loader;

        public AddTeamMemberPage(IPage page)
        {
            _page = page;
            _firstNameTxt = _page.Locator("#firstName");
            _lastNameTxt = _page.Locator("#lastName");
            _emailTxt = _page.Locator("#email");
            _phoneTxt = _page.Locator("#phone");
            _userNameTxt = _page.Locator("#username");
            _passwordTxt = _page.Locator("#password");
            _confirmPasswordTxt = _page.Locator("#confirmPassword");
            _pageTitle = _page.Locator(".pageTitle.text-center");
            _saveAndCloseBtn = _page.Locator("(//button[@type='button'][not(@disabled)])[last()]");
            _savedMessageToastr = _page.Locator(".alert.alert-success.show.fade.alert-dismissible");
            _studentsRecordList = _page.Locator("table.studentsTable tbody tr");
            _deleteBtn = _page.Locator("button.btn-danger");
            _confirmPopupDeleteBtn = _page.Locator("div.modal-body button.btn-danger");
            _confirmPopupMessage = _page.Locator("div.modal-body p:not([style*='word'])");
            _loader = _page.Locator("em.fa-spinner");
        }

        public async Task FillFirstNameAsync(string firstName) => await _firstNameTxt.FillAsync(firstName);

        public async Task FillLastNameAsync(string lastName) => await _lastNameTxt.FillAsync(lastName);

        public async Task FillEmailAsync(string email) => await _emailTxt.FillAsync(email);

        public async Task FillMobileNumberAsync(string mobileNumber) => await _phoneTxt.FillAsync(mobileNumber);

        public async Task FillUserNameAsync(string userName) => await _userNameTxt.FillAsync(userName);

        public async Task FillPasswordAsync(string password) => await _passwordTxt.FillAsync(password);

        public async Task FillConfirmPasswordAsync(string confirmPassword) => await _confirmPasswordTxt.FillAsync(confirmPassword);

        public async Task ClickSaveAndCloseAsync() => await _saveAndCloseBtn.ClickAsync();

        public async Task ClickDeleteBtnAsync() => await _deleteBtn.ClickAsync();

        public ILocator GetDeletePopupMessage() => _confirmPopupMessage;

        public async Task ClickConfirmDeleteBtnAsync() => await _confirmPopupDeleteBtn.ClickAsync();

        public ILocator GetPageTitle() => _pageTitle;

        public ILocator GetSavedStudentRecord() => _savedMessageToastr;

        public ILocator WaitForLoaderInvisible() => _loader;

        public async Task CreateNewTeamMemberAsync(string firstName, string lastName, string email, string phoneNumber, string userName, string password)
        {
            await FillFirstNameAsync(firstName);
            await FillLastNameAsync(lastName);
            await FillEmailAsync(email);
            await FillMobileNumberAsync(phoneNumber);
            await FillUserNameAsync(userName);
            await FillPasswordAsync(password);
            await FillConfirmPasswordAsync(password);
            await ClickSaveAndCloseAsync();
        }
    }
}
