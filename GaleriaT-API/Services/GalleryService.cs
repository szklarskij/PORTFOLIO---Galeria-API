using AutoMapper;
using GaleriaT_API.Entities;
using GaleriaT_API.Models;
using GaleriaT_API.Exceptions;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;
using Microsoft.Extensions.Hosting;
using System.Drawing;
using static System.Net.Mime.MediaTypeNames;
using Image = System.Drawing.Image;
using System.Drawing.Imaging;
using System.Drawing.Drawing2D;
using System.IO;
using System.Linq;

namespace GaleriaT_API.Services
{






    public interface IGalleryService
    {
        int Create(CreateGalleryPostDto dto, IFormFile file, IFormFile file2, IFormFile file3);
        List<ThumbnailGalleryDto> GetAllThumbnails(GalleryQuery query);
        PageResult<GalleryPostDto> GetAll(GalleryQuery query);

        GalleryPostDto GetById(int id);
        void Delete(int id);
        void Update(int id, UpdateGalleryPostDto dto, IFormFile file, IFormFile file2, IFormFile file3);
        public void UpdateOrder(int id, int id2);
        public void DeleteAll();

    }

    public class GalleryService : IGalleryService
    {
        private readonly GalleryDbContext _dbContext;
        private readonly IMapper _mapper;
        private readonly ILogger<GalleryService> _logger;

        public GalleryService(GalleryDbContext dbContext, IMapper mapper, ILogger<GalleryService> logger)
        {
            _dbContext = dbContext;
            _mapper = mapper;
            _logger = logger;
        }


        public void Update(int id, UpdateGalleryPostDto dto, IFormFile file, IFormFile file2, IFormFile file3)
        {
            var post = _dbContext
                 .GalleryPosts
                 .FirstOrDefault(g => g.Id == id);

            if (post is null) throw new NotFoundException("Post not found");
            //if (file == null || file.Length == 0) throw new BadRequestExcepion("No file attached.");

            var rootPath = Directory.GetCurrentDirectory();
            var fullPath = $"{rootPath}/wwwroot/{post.ImageUrl}";
            var thumbFullPath = $"{rootPath}/wwwroot/{post.ThumbImageUrl}";
            var smallFullPath = $"{rootPath}/wwwroot/{post.SmallImageUrl}";




            if (file != null && file2!=null && file3 != null)
            {
                {
                    Guid guid = Guid.NewGuid();
                    string extension = Path.GetExtension(file.FileName);


                    string fileName = guid.ToString() + extension;
                    var fullPathNewFile = $"{rootPath}/wwwroot/img/{fileName}";
                    var fullPathNewFile2 = $"{rootPath}/wwwroot/img/SMALL_{fileName}";
                    var fullPathNewFile3 = $"{rootPath}/wwwroot/img/THUMB_{fileName}";


                    using (var stream = new FileStream(fullPathNewFile, FileMode.Create))
                    {
                        file.CopyTo(stream);
                    }
                    using (var stream2 = new FileStream(fullPathNewFile2, FileMode.Create))
                    {
                        file2.CopyTo(stream2);
                    }
                    using (var stream3 = new FileStream(fullPathNewFile3, FileMode.Create))
                    {
                        file3.CopyTo(stream3);
                    }
                    //using (var stream = new FileStream(fullPathNewFile, FileMode.Open))
                    //{
                    //    const int smallSize = 300;
                    //    const int thumbnailSize = 96;
                    //    try
                    //    {

                    //        Image imageToResize = Image.FromStream(stream);
                    //        Image imageToResize2 = Image.FromStream(stream);

                    //        Image smallImage = ScaleImage.Scale(imageToResize, smallSize);
                    //        var path = $"{rootPath}/wwwroot/img/SMALL_{fileName}";
                    //        smallImage.Save($"{path}");
                    //        Image thumbImage = ScaleImage.Scale(imageToResize2, thumbnailSize);
                    //        var path2 = $"{rootPath}/wwwroot/img/THUMB_{fileName}";
                    //        thumbImage.Save($"{path2}");
                    //    }
                    //    catch (Exception e)
                    //    {
                    //        stream.Close();
                    //        File.Delete(fullPathNewFile);
                    //        throw new BadImageException($"Image file error. Try conver to 24 bit");
                    //    }

                    File.Delete(fullPath);
                    File.Delete(thumbFullPath);
                    File.Delete(smallFullPath);

                    post.ImageUrl = $"/img/{fileName}";
                    post.ThumbImageUrl = $"/img/THUMB_{fileName}";
                    post.SmallImageUrl = $"/img/SMALL_{fileName}";

                }


            }
        
            post.Title = dto.Title;
            post.DateOfWork = dto.DateOfWork;
            post.Text = dto.Text;
            post.Price = dto.Price;
            post.Size = dto.Size;
            post.SizeMultiplied = dto.SizeMultiplied;
            post.Technique = dto.Technique;
            post.Tag = dto.Tag;

            _dbContext.SaveChanges();


        }
        public void UpdateOrder(int id, int id2)
        {
            var post = _dbContext
                 .GalleryPosts
                 .FirstOrDefault(g => g.Id == id);
            if (post is null) throw new NotFoundException("Post not found");
            var postToSwap = _dbContext
            .GalleryPosts
             .FirstOrDefault(g => g.Id == id2);
            if (postToSwap is null) throw new NotFoundException("Post to swap not found");

   

            var tempPost = new GalleryPost();
            tempPost.Title = post.Title;
            tempPost.Text = post.Text;
            tempPost.CreatedDate = post.CreatedDate;
            tempPost.DateOfWork = post.DateOfWork;
            tempPost.ImageUrl = post.ImageUrl;
            tempPost.SmallImageUrl = post.SmallImageUrl;
            tempPost.ThumbImageUrl = post.ThumbImageUrl;
            tempPost.Price = post.Price;
            tempPost.Size = post.Size;

            post.Title = postToSwap.Title;
            post.Text = postToSwap.Text;
            post.CreatedDate = postToSwap.CreatedDate;
            post.DateOfWork = postToSwap.DateOfWork;
            post.ImageUrl = postToSwap.ImageUrl;
            post.SmallImageUrl = postToSwap.SmallImageUrl;
            post.ThumbImageUrl = postToSwap.ThumbImageUrl;
            post.Price = postToSwap.Price;
            post.Size = postToSwap.Size;

            postToSwap.Title = tempPost.Title;
            postToSwap.Text = tempPost.Text;
            postToSwap.CreatedDate = tempPost.CreatedDate;
            postToSwap.DateOfWork = tempPost.DateOfWork;
            postToSwap.ImageUrl = tempPost.ImageUrl;
            postToSwap.SmallImageUrl = tempPost.SmallImageUrl;
            postToSwap.ThumbImageUrl = tempPost.ThumbImageUrl;
            postToSwap.Price = tempPost.Price;
            postToSwap.Size = tempPost.Size;

            _dbContext.SaveChanges();


        }

        public void Delete(int id)
        {
            _logger.LogError($"Gallery post with id: {id} DELETE action invoked");
            var post = _dbContext
                 .GalleryPosts
                 .FirstOrDefault(g => g.Id == id);
            if (post is null) throw new NotFoundException("Post not found"); 

            var rootPath = Directory.GetCurrentDirectory();
            var fullPath = $"{rootPath}/wwwroot/{post.ImageUrl}";
            var thumbFullPath = $"{rootPath}/wwwroot/{post.ThumbImageUrl}";
            var smallFullPath = $"{rootPath}/wwwroot/{post.SmallImageUrl}";



            if (post.ImageUrl != null)
            {

                if (File.Exists(fullPath)) File.Delete(fullPath);

            }
            if (post.SmallImageUrl != null)
            {

                if (File.Exists(smallFullPath)) File.Delete(smallFullPath);

            }
            if (post.ThumbImageUrl != null)
            {

                if (File.Exists(thumbFullPath)) File.Delete(thumbFullPath);

            }


            _dbContext.GalleryPosts.Remove(post);
            _dbContext.SaveChanges();
        }
        public void DeleteAll()
        {
            _logger.LogError($"DELETE ALL action invoked");
            var rootPath = Directory.GetCurrentDirectory();
   
            foreach (var post in _dbContext
                 .GalleryPosts)
            {
            var fullPath = $"{rootPath}/wwwroot/{post.ImageUrl}";
                var thumbFullPath = $"{rootPath}/wwwroot/{post.ThumbImageUrl}";
                var smallFullPath = $"{rootPath}/wwwroot/{post.SmallImageUrl}";
            if (post.ImageUrl != null)
            {
                if (File.Exists(fullPath)) File.Delete(fullPath);
            }
            if (post.ThumbImageUrl != null)
                {
                    if (File.Exists(thumbFullPath)) File.Delete(thumbFullPath);
                }
                if (post.SmallImageUrl != null)
            {
                if (File.Exists(smallFullPath)) File.Delete(smallFullPath);
            }
                _dbContext
                 .GalleryPosts.Remove (post);
            }
 
            _dbContext.SaveChanges();
        }


        public GalleryPostDto GetById(int id)


        {
            var post = _dbContext
               .GalleryPosts
               .FirstOrDefault(g => g.Id == id);

            if (post is null) throw new NotFoundException("Post not found"); ;

            var result = _mapper.Map<GalleryPostDto>(post);
   
            return result;

        }
        public PageResult<GalleryPostDto> GetAll(GalleryQuery query)
        {
            List<string> searchTags = new List<string>();
                var baseQuery = _dbContext.GalleryPosts.Select(r => r);
            if (query.SearchPhrase != null)
            {

                var wordList = query.SearchPhrase.ToLower().Split(" ").ToList();
                wordList.ForEach(l => { if (l.Length > 2) searchTags.Add(l); });

                searchTags.ForEach(tag =>
                {
                   var  query = _dbContext.GalleryPosts.Where(r => r.Tag.ToLower().Contains(tag.ToLower()) || r.Title.ToLower().Contains(tag.ToLower())
                    || r.Text.ToLower().Contains(tag.ToLower()) || r.Technique.ToLower().Contains(tag.ToLower()));
                    baseQuery = query.Intersect(baseQuery);

                });

            }

            if (!string.IsNullOrEmpty(query.SortBy))
            {
                var columnsSelector = new Dictionary<string, Expression<Func<GalleryPost, object>>>
                {
                    {nameof(GalleryPost.Id), r=>r.Id },

                    {nameof(GalleryPost.Title), r=>r.Title },
                    {nameof(GalleryPost.DateOfWork), r=>r.DateOfWork },
                    {nameof(GalleryPost.CreatedDate), r=>r.CreatedDate },
                    {nameof(GalleryPost.SizeMultiplied), r=>r.SizeMultiplied }

                };

                var selectedColumn = columnsSelector[query.SortBy];

                baseQuery = query.SortDirection == SortDirection.ASC
                    ? baseQuery.OrderBy(selectedColumn)
                    : baseQuery.OrderByDescending(selectedColumn);
            }

            var gallery = baseQuery
               .Skip(query.PageSize * (query.PageNumber - 1))
               .Take(query.PageSize)
               .ToList();

            var totalItemsCount = baseQuery.Count();

            var galleryDto = _mapper.Map<List<GalleryPostDto>>(gallery);

            var result = new PageResult<GalleryPostDto>(galleryDto, totalItemsCount, query.PageSize, query.PageNumber);

            return result;
        }

        public List<ThumbnailGalleryDto> GetAllThumbnails(GalleryQuery query)
        {

            List<string> searchTags = new List<string>();
            var baseQuery = _dbContext.GalleryPosts.Select(r => r);
            if (query.SearchPhrase != null)
            {

                var wordList = query.SearchPhrase.ToLower().Split(" ").ToList();
                wordList.ForEach(l => { if (l.Length > 2) searchTags.Add(l); });

                searchTags.ForEach(tag =>
                {
                    var query = _dbContext.GalleryPosts.Where(r => r.Tag.ToLower().Contains(tag.ToLower()) || r.Title.ToLower().Contains(tag.ToLower())
                     || r.Text.ToLower().Contains(tag.ToLower()) || r.Technique.ToLower().Contains(tag.ToLower()));
                    baseQuery = query.Intersect(baseQuery);

                });

            }

            if (!string.IsNullOrEmpty(query.SortBy))
            {
                var columnsSelector = new Dictionary<string, Expression<Func<GalleryPost, object>>>
                {
                    {nameof(GalleryPost.Id), r=>r.Id },
                    {nameof(GalleryPost.Title), r=>r.Title },
                    {nameof(GalleryPost.DateOfWork), r=>r.DateOfWork },
                    {nameof(GalleryPost.CreatedDate), r=>r.CreatedDate },
                    {nameof(GalleryPost.SizeMultiplied), r=>r.SizeMultiplied }

                };

                var selectedColumn = columnsSelector[query.SortBy];

                baseQuery = query.SortDirection == SortDirection.ASC
                    ? baseQuery.OrderBy(selectedColumn)
                    : baseQuery.OrderByDescending(selectedColumn);
            }

            var gallery = baseQuery
               .ToList();

            var totalItemsCount = baseQuery.Count();

            var galleryDto = _mapper.Map<List<ThumbnailGalleryDto>>(gallery);


            return galleryDto;
        }


        public int Create(CreateGalleryPostDto dto, IFormFile file, IFormFile file2, IFormFile file3)
        {
            if (file == null || file.Length == 0) throw new BadRequestExcepion("No file attached.");
            if (file2 == null || file2.Length == 0) throw new BadRequestExcepion("No file attached.");
            if (file3 == null || file2.Length == 0) throw new BadRequestExcepion("No file attached.");


            string ext = Path.GetExtension(file.FileName.ToLower());
            if (ext != ".webp") throw new BadRequestExcepion("Wrong file extension. Must be webp.");

            var rootPath = Directory.GetCurrentDirectory();

            Guid guid = Guid.NewGuid();
            string extension = Path.GetExtension(file.FileName);
            string fileName = guid.ToString()+ extension;

            var fullPath = $"{rootPath}/wwwroot/img/{fileName}";
            var fullPath2 = $"{rootPath}/wwwroot/img/SMALL_{fileName}";
            var fullPath3 = $"{rootPath}/wwwroot/img/THUMB_{fileName}";




            using (var stream = new FileStream(fullPath, FileMode.Create))
            {
                file.CopyTo(stream);
                stream.Close();
            }
            using (var stream2 = new FileStream(fullPath2, FileMode.Create))
            {
                file2.CopyTo(stream2);
                stream2.Close();
            }
            using (var stream3 = new FileStream(fullPath3, FileMode.Create))
            {
                file3.CopyTo(stream3);
                stream3.Close();
            }



            //using (var stream2 = new FileStream(fullPath, FileMode.Open))
            //{
            //    const int smallSize = 300;
            //    const int thumbnailSize = 96;

            //    try
            //    {
            //        Image imageToResize = Image.FromStream(stream2);
            //        Image imageToResize2 = Image.FromStream(stream2);

            //        Image smallImage = ScaleImage.Scale(imageToResize, smallSize);
            //        var path = $"{rootPath}/wwwroot/img/SMALL_{fileName}";
            //        smallImage.Save($"{path}");
            //        Image thumbImage = ScaleImage.Scale(imageToResize2, thumbnailSize);
            //        var path2 = $"{rootPath}/wwwroot/img/THUMB_{fileName}";
            //        thumbImage.Save($"{path2}");
            //    }


            //    catch (Exception e)
            //    {
            //        stream2.Close();


            //        File.Delete(fullPath);
            //        throw new BadImageException($"Image file error");
            //    }
            //}
            dto.ImageUrl = $"/img/{fileName}";
            dto.SmallImageUrl = $"/img/SMALL_{fileName}";
            dto.ThumbImageUrl = $"/img/THUMB_{fileName}";

            var galleryPost = _mapper.Map<GalleryPost>(dto);
                _dbContext.GalleryPosts.Add(galleryPost);
                _dbContext.SaveChanges();
                return galleryPost.Id;
        }
    }
}