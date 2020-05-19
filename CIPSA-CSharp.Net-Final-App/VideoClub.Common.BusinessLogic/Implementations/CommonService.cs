using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VideoClub.Common.BusinessLogic.Dto;
using VideoClub.Common.Model.Enums;
using VideoClub.Common.Model.Utils;

namespace VideoClub.Common.BusinessLogic.Implementations
{
    public class CommonService
    {
        public static ProductDto GetProduct(string productId)
        {
            ProductDto product;
            if (productId.Contains(CommonHelper.Movie))
            {
                product = MovieService.Instance.Get(productId);
            }
            else
            {
                product = VideoGameService.Instance.Get(productId);
            }

            return product;
        }

        public static ProductDto HasBeenChangeStateProduct(RentalDto rentalDto, StateProductEnum stateNew, out bool result)
        {
            ProductDto product;
            if (rentalDto.ProductId.Contains(CommonHelper.Movie))
            {
                product = MovieService.Instance.Get(rentalDto.ProductId);
                result = MovieService.Instance.HasBeenChangeState((MovieDto)product, stateNew);
            }
            else
            {
                product = VideoGameService.Instance.Get(rentalDto.ProductId);
                result = VideoGameService.Instance.HasBeenChangeState((VideoGameDto)product, stateNew);
            }

            return product;
        }
    }
}
