using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataLayer.DTO;

namespace DataLayer
{
    public interface IAdRepository
    {
        int InsertAd(Ad ad);
        List<Category> GetCategories();
        List<Ad> GetAllAds();
        List<Ad> SearchAds(string searchTerm, int ?categoryId, string city, string locality, int priceLow, int priceHigh, string sortBy);
        Ad GetAdById(int adId);
        int SaveAdImages(List<byte[]> images,int adId);

        List<byte[]> GetAdImages(int adId);
    }
}
