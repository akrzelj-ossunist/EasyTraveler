﻿using ET.Application.Models.BusDtos;
using ET.Application.Models.BusDtos.Response;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ET.Application.Services
{
    public interface BusService
    {
        public BusResponseDto Create(BusDto busDto);
        public BusResponseDto Update(Guid id, BusDto busDto);
        public bool Delete(Guid id);
        public BusResponseDto GetById(Guid id);
        public int GetTotal(Dictionary<string, string> searchParams);
        public List<BusResponseDto> GetAll();
        public List<BusResponseDto> Filter(BusPageDto busPageDto, Dictionary<string, string> searchParams);
    }
}
