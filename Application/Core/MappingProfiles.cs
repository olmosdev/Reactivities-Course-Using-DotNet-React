using System;
using AutoMapper;
using Domain;

namespace Application.Core;

public class MappingProfiles : Profile
{
    // ctor (shorcut)
    public MappingProfiles()
    {
        // We need to tell Automapper where we're going to map from and what we're going to map to
        CreateMap<Activity, Activity>();
    }
}
