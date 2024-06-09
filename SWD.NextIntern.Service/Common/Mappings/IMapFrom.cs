using AutoMapper;

namespace SWD.NextIntern.Service.Common.Mappings
{
    interface IMapFrom<T>
    {
        void Mapping(Profile profile);
    }
}
