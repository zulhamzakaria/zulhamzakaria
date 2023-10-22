using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Forms;
using PlannerApp.Client.Services.Exceptions;
using PlannerApp.Client.Services.Interfaces;
using PlannerApp.Shared;
using PlannerApp.Shared.Models;
using System.Linq.Expressions;

namespace PlannerApp.Components;

public partial class PlanForm
{
    [Inject]
    public IPlansService? PlansService { get; set; }

    [Inject]
    public NavigationManager? NavigationManager { get; set; }

    [Parameter]
    public string? Id { get; set; }

    [CascadingParameter]
    public Error Error { get; set; }    

    private bool _isEditMode => Id is not null;
    private PlanDetail _planDetail = new();
    private bool _isBusy = false;
    private Stream? _stream = null;
    private string _fileName = string.Empty;
    private string _errorMessage = string.Empty;

    protected override async Task OnInitializedAsync()
    {
        if(_isEditMode)
            await FetchPlanByIdAsync();
    }

    private async Task SubmitFormAsync()
    {
        _isBusy = true;

        try
        {
            FormFile? formFile = null;
            if (_stream is not null)
                formFile = new FormFile(_stream, _fileName);

            if(_isEditMode)
                await PlansService!.EditAsync(_planDetail, formFile!);
            else
                await PlansService!.CreateAsync(_planDetail, formFile!);

            NavigationManager?.NavigateTo("/plans");
        }
        catch(ApiException ex)
        {
            _errorMessage = ex.ApiErrorResponse!.Message!;
        }
        catch (Exception ex)
        {
            Error.HandleError(ex);
        }

        _isBusy = false;
    }

    private async Task FetchPlanByIdAsync()
    {
        _isBusy = true;
        try
        {
            var result = await PlansService!.GetByIdAsync(Id);
            _planDetail = result.Value!;

        }
        catch(ApiException ex)
        {
            _errorMessage = ex.ApiErrorResponse?.Message!;
        }
        catch (Exception ex)
        {
            _errorMessage = ex.Message;
        }
        _isBusy = false;
    }
    private async Task OnChooseFileAsync(InputFileChangeEventArgs e)
    {
        var file = e.File;
        if (file == null)
        {
            _errorMessage = "No file uploaded";
            return;
        }

        // limit = 2MB
        if (file.Size > 2_097_152)
        {
            _errorMessage = "File must be lesser or equal to 2MB";
            return;
        }


        string[] allowedExtensions = new[] { ".jpg", ".png", ".bmp", ".svg" };
        string extension = Path.GetExtension(file.Name).ToLower();
        if (!allowedExtensions.Contains(extension))
        {
            _errorMessage = "Invalid image file";
            return;
        }

        // limit the allocated mem to 2MB only
        using(var stream = e.File.OpenReadStream(2_097_152))
        {
            var buffer = new byte[file.Size];
            await stream.ReadAsync(buffer, 0, (int)file.Size);
            _stream  = new MemoryStream(buffer);
            _stream.Position = 0;
            _fileName = file.Name;
        }

    }


}