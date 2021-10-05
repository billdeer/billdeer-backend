using AutoMapper;
using Billdeer.Business.Constants;
using Billdeer.Core.Entities.Concrete;
using Billdeer.Core.Utilities.Results;
using Billdeer.Core.Utilities.Results.ComplexTypes;
using Billdeer.Core.Utilities.ToolKit;
using Billdeer.DataAccess.Abstract;
using Billdeer.Entities.Concrete;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Billdeer.Business.Handlers.Freelancers.Commands
{
    public class CreateFreelancerCommand : IRequest<IDataResult<Freelancer>>
    {
        public long UserId { get; set; }

        public class CreateFreelancerCommandHandler : IRequestHandler<CreateFreelancerCommand, IDataResult<Freelancer>>
        {
            private readonly IFreelancerRepository _freelancerRepository;
            private readonly IUserRepository _userRepository;
            private readonly IMapper _mapper;

            public CreateFreelancerCommandHandler(IFreelancerRepository freelancerRepository, IUserRepository userRepository, IMapper mapper)
            {
                _freelancerRepository = freelancerRepository;
                _userRepository = userRepository;
                _mapper = mapper;
            }

            public async Task<IDataResult<Freelancer>> Handle(CreateFreelancerCommand request, CancellationToken cancellationToken)
            {
                if (!IfEngine
                    .Engine(
                        CheckEntities<IUserRepository, User>.Exist(_userRepository, request.UserId),
                        CheckAlreadyFreelancer(request.UserId)
                        )
                    )
                {
                    return new DataResult<Freelancer>(ResultStatus.Warning, Messages.UserNotFound);
                }

                var freelancer = _mapper.Map<Freelancer>(request);

                _freelancerRepository.Add(freelancer);
                await _freelancerRepository.SaveChangesAsync();

                return new DataResult<Freelancer>(freelancer, ResultStatus.Success, Messages.Added);
            }

            private bool CheckAlreadyFreelancer(long userId)
            {
                var result = _freelancerRepository.Queryable(x => x.UserId == userId && x.IsActive == true && x.IsDeleted == false);
                return result.Any();
            }

        }
    }
}
