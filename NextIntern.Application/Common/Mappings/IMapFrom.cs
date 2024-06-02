using AutoMapper;

namespace NextIntern.Application.Common.Mappings
{
    interface IMapFrom<T>
    {
        void Mapping(Profile profile);
    }
}
