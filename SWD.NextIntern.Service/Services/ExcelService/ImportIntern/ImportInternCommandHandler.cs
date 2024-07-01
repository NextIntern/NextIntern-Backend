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
        private readonly IRoleRepository _roleRepository;
        private readonly ICampaignRepository _campaignRepository;

        public ImportInternCommandHandler(IUserRepository userRepository, IRoleRepository roleRepository, ICampaignRepository campaignRepository)
        {
            _userRepository = userRepository;
            _roleRepository = roleRepository;
            _campaignRepository = campaignRepository;
        }

        public async Task<ResponseObject<string>> Handle(ImportInternCommand request, CancellationToken cancellationToken)
        {
            string defaultPassword = "123456";

            var campaign = await _campaignRepository.FindAsync(c => c.CampaignId.ToString().Equals(request.CampaignId) && c.DeletedDate == null, cancellationToken);

            if (campaign == null)
            {
                return new ResponseObject<string>(HttpStatusCode.NotFound, $"Campaign with id {request.CampaignId} does not exist!");
            }

            var role = await _roleRepository.FindAsync(r => r.RoleName.Equals("User") && r.DeletedDate == null, cancellationToken);

            if (role == null)
            {
                return new ResponseObject<string>(HttpStatusCode.NotFound, "Role does not exist!");
            }

            var interns = new List<User>();

            using (var stream = new MemoryStream())
            {
                await request.File.CopyToAsync(stream, cancellationToken);
                using (var package = new ExcelPackage(stream))
                {
                    var worksheet = package.Workbook.Worksheets[0];

                    var range = worksheet.Cells[worksheet.Dimension.Address];

                    interns = range.ToCollectionWithMappings<User>(row =>
                    {
                        if (string.IsNullOrEmpty(row.GetText("Id")))
                        {
                            return null;
                        }

                        if (string.IsNullOrEmpty(row.GetText("FullName")))
                        {
                            return null;
                        }

                        if (string.IsNullOrEmpty(row.GetText("Dob")))
                        {
                            return null;
                        }

                        if (string.IsNullOrEmpty(row.GetText("Gender")))
                        {
                            return null;
                        }

                        if (string.IsNullOrEmpty(row.GetText("Telephone")))
                        {
                            return null;
                        }

                        if (string.IsNullOrEmpty(row.GetText("Email")))
                        {
                            return null;
                        }

                        if (string.IsNullOrEmpty(row.GetText("Address")))
                        {
                            return null;
                        }
                        //add check field null or empty
                        //add intern vao` campaign

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
                            RoleId = role.RoleId,
                            CampaignId = Guid.Parse(request.CampaignId)
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
