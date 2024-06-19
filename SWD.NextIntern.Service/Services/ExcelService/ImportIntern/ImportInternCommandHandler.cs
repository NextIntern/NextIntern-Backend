using MediatR;
using OfficeOpenXml;
using SWD.NextIntern.Repository.Entities;
using SWD.NextIntern.Repository.Repositories.IRepositories;
using SWD.NextIntern.Service.DTOs.Responses;
using System.Net;

namespace SWD.NextIntern.Service.Services.ExcelService.ImportIntern
{
    public class ImportInternCommandHandler : IRequestHandler<ImportInternCommand, ResponseObject<string>>
    {
        private readonly IUserRepository _userRepository;

        public ImportInternCommandHandler(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public async Task<ResponseObject<string>> Handle(ImportInternCommand request, CancellationToken cancellationToken)
        {
            var interns = new List<User>();
            string defaultPassword = "123456";

            using (var stream = new MemoryStream())
            {
                await request.File.CopyToAsync(stream, cancellationToken);
                using (var package = new ExcelPackage(stream))
                {
                    var worksheet = package.Workbook.Worksheets[0];

                    var range = worksheet.Cells[worksheet.Dimension.Address];

                    interns = range.ToCollectionWithMappings<User>(row =>
                    {
                        var user = new User
                        {
                            FullName = row.GetText("FullName"),
                            Dob = DateOnly.TryParse(row.GetText("Dob"), out DateOnly dob) ? dob : null,
                            Gender = row.GetText("Gender"),
                            Telephone = row.GetText("Telephone"),
                            Email = row.GetText("Email"),
                            Address = row.GetText("Address"),
                            Username = row.GetText("Email"),
                            Password = BCrypt.Net.BCrypt.HashPassword(defaultPassword),
                            RoleId = Guid.Parse("bcc9620f-02c5-4eb1-a8c0-ecbe0a882e66")
                        };

                        return user;

                    }, options => options.HeaderRow = 0);
                }
            }

            foreach (var intern in interns)
            {
                _userRepository.Add(intern);
            }

            return await _userRepository.UnitOfWork.SaveChangesAsync(cancellationToken) > 0 ? new ResponseObject<string>(HttpStatusCode.Created, "Success!") : new ResponseObject<string>(HttpStatusCode.BadRequest, "Fail!");
        }
    }
}
