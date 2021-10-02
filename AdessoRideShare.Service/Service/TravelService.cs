using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using AdessoRideShare.Core.DTOs;
using AdessoRideShare.Core.Entities;
using AdessoRideShare.Core.Repositories;
using AdessoRideShare.Core.Services;
using AdessoRideShare.Core.UnitOfWork;
using AdessoRideShare.Service.Mapping;
using AdessoRideShare.Shared.DTOs;

namespace AdessoRideShare.Service.Service
{
    public class TravelService : ITravelService
    {
        private readonly IUnitOfWork _unitOfWork;

        private readonly IRepository<TravelPlan> _travelRepository;

        public TravelService(IUnitOfWork unitOfWork, IRepository<TravelPlan> travelRepository)
        {
            _unitOfWork = unitOfWork;
            _travelRepository = travelRepository;
        }
        public async Task<Response<IEnumerable<TravelPlanDto>>> GetAllTravelPlan()
        {
            var allTravelPlan = ObjectMapper.Mapper.Map<List<TravelPlanDto>>(await _travelRepository.GetAllAsync());

            return Response<IEnumerable<TravelPlanDto>>.Success(allTravelPlan, 200);
        }

        public async Task<Response<TravelPlanDto>> AddTravelPlanAsync(TravelPlanDto travelPlan)
        {
            var newTravelPlan = ObjectMapper.Mapper.Map<TravelPlan>(travelPlan);
            newTravelPlan.Id = Guid.NewGuid();
            List<string> neighbourList = new List<string>();

            int rowFrom = travelPlan.From / 100;
            int indexFrom = travelPlan.From % 100;

            int rowWhere = travelPlan.Where / 100;
            int indexWhere = travelPlan.Where % 100;

            if(travelPlan.Where > travelPlan.From)
            {
                var difference = rowWhere - rowFrom;
                if (difference > 0)
                {
                    for(int i = rowFrom +1; i <= rowWhere; i++)
                    {
                        int newNeighbour = i * 100 + indexFrom;
                        neighbourList.Add(newNeighbour.ToString());
                    }
                    if (indexFrom > indexWhere)
                    {
                        for (int i = indexFrom; i >= indexWhere; i--)
                        {
                            int newNeighbour = rowWhere * 100 + i;
                            neighbourList.Add(newNeighbour.ToString());
                        }
                    }
                    else if (indexFrom < indexWhere)
                    {
                        for (int i = indexWhere; i >= indexFrom; i--)
                        {
                            int newNeighbour = rowWhere * 100 + i;
                            neighbourList.Add(newNeighbour.ToString());
                        }
                    }

                }
                else
                {
                    if(indexFrom > indexWhere)
                    {
                        for (int i = indexFrom; i >= indexWhere; i++)
                        {
                            int newNeighbour = rowWhere * 100 + i;
                            neighbourList.Add(newNeighbour.ToString());
                        }
                    }
                    else if(indexFrom < indexWhere)
                    {
                        for(int i = indexWhere; i >= indexFrom; i--)
                        {
                            int newNeighbour = rowWhere * 100 + i;
                            neighbourList.Add(newNeighbour.ToString());
                        }
                    }
                }
            }
            else
            {
                var difference = rowWhere - rowFrom;

                if(difference > 0)
                {
                    for (int i = rowWhere -1; i >= rowWhere; i--)
                    {
                        int newNeighbour = i * 100 + indexFrom;
                        neighbourList.Add(newNeighbour.ToString());
                    }
                    if (indexFrom > indexWhere)
                    {
                        for (int i = indexFrom; i <= indexWhere; i++)
                        {
                            int newNeighbour = indexWhere * 100 + i;
                            neighbourList.Add(newNeighbour.ToString());
                        }
                    }
                    else if (indexFrom < indexWhere)
                    {
                        for (int i = indexWhere; i >= indexFrom; i--)
                        {
                            int newNeighbour = indexWhere * 100 + i;
                            neighbourList.Add(newNeighbour.ToString());
                        }
                    }
                }
                else
                {
                    if (indexFrom > indexWhere)
                    {
                        for (int i = indexFrom; i <= indexWhere; i++)
                        {
                            neighbourList.Add(i.ToString());
                        }
                    }
                    else if (indexFrom < indexWhere)
                    {
                        for (int i = indexWhere; i >= indexFrom; i--)
                        {
                            neighbourList.Add(i.ToString());
                        }
                    }
                }
            }

            newTravelPlan.ConnectedCityList = String.Join(",", neighbourList);

            await _travelRepository.AddAsync(newTravelPlan);

            await _unitOfWork.CommmitAsync();

            var newDto = ObjectMapper.Mapper.Map<TravelPlanDto>(newTravelPlan);

            return Response<TravelPlanDto>.Success(newDto, 200);
        }

        public async Task<Response<TravelPlanDto>> JoinTravelPlan(Guid travelPlanId)
        {
            var travelPlan = await _travelRepository.GetByIdAsync(travelPlanId);

            if(travelPlan.SeatCapacity > 0)
            {
                travelPlan.SeatCapacity -= 1;

                _travelRepository.Update(travelPlan);

                _unitOfWork.Commit();

                return Response<TravelPlanDto>.Success(204);
            }
            else
            {
                return Response<TravelPlanDto>.Fail("No capacity for selected travel plan", 200, true);
            }
        }

        public async Task<Response<TravelPlanDto>> PublishTravelPlan(Guid id)
        {
            var entity = await _travelRepository.GetByIdAsync(id);

            entity.isPublish = true;

            _travelRepository.Update(entity);

            _unitOfWork.Commit();

            return Response<TravelPlanDto>.Success(204); //Guncelleme isinde NoContent donuyorum.
        }

        public Task<Response<IEnumerable<TravelPlanDto>>> SearchTravelPlan(int from, int where)
        {
            var searchedTravelPlans = _travelRepository.GetAllAsync().Result.Where(w => w.Where == where && w.From == from).ToList();

            return Task.Run(() =>Response<IEnumerable<TravelPlanDto>>.Success(ObjectMapper.Mapper.Map<List<TravelPlanDto>>(searchedTravelPlans), 200));
        }

        public async Task<Response<TravelPlanDto>> UnpublishTravelPlan(Guid id)
        {
            var entity = await _travelRepository.GetByIdAsync(id);

            entity.isPublish = false;

            _travelRepository.Update(entity);

            _unitOfWork.Commit();

            return Response<TravelPlanDto>.Success(204); //Guncelleme isinde NoContent donuyorum.
        }

        public Task<Response<IEnumerable<TravelPlanDto>>> SearchTravelPlanWithConnectedCity(int from, int where)
        {
            var searchedTravelPlans = _travelRepository.GetAllAsync().Result.Where(w => w.ConnectedCityList.Contains(where.ToString()) && w.ConnectedCityList.Contains(from.ToString())).ToList();

            return Task.Run(() => Response<IEnumerable<TravelPlanDto>>.Success(ObjectMapper.Mapper.Map<List<TravelPlanDto>>(searchedTravelPlans), 200));
        }
    }
}
