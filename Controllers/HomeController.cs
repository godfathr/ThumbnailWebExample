using ImageThumbnailCreator;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ThumbnailWebExample.Controllers
{
    public class HomeController : Controller
    {
        //Create an instance of the JpegThumbnailer from the "ImageThumbnailCreator" NuGet package
        private static Thumbnailer _thumbnailer = new Thumbnailer();

        //Define the path to the base path of your web project
        private static string projectImageFolder = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, @"Content\sampleImages\");

        //Define the folder in your project where the thumbnail will be placed
        private static string projectFolder = @"Content/sampleImages/";
        private static string saveOriginalFileFolder = @"Content/OriginalSaved/";

        //File name(s) for the file you wish to create a thumbnail for.
        //The file(s) would typically come from the form POST request 
        /*
         * e.g. Request.Files[0] for the first file 
         *  if (Request.Files.Count > 0)
            {
                HttpPostedFileBase photo = Request.Files[0];
                _thumbnailer.SaveOriginal(saveOriginalFileFolder, photo);
                ...            
                //...then process the thumbnails like below
                ...
            }
        */
        private static string largeJpegLandscape = @"largeLandscape.jpg";
        private static string largeJpegPortrait = @"largePortrait.jpg";
        private static string largeJpegSquare = @"largeSquare.jpg";

        public ActionResult Index()
        {
            //Define the path to all the sample images
            string largeJpegLandscapePath = Path.Combine(projectImageFolder, largeJpegLandscape);
            string largeJpegPortraitPath = Path.Combine(projectImageFolder, largeJpegPortrait);
            string largeJpegSquarePath = Path.Combine(projectImageFolder, largeJpegSquare);

            //Choose the width of your thumbnail. The height will be auto calculated to keep the aspect ratio intact
            float width = 300;

            //Create the thumbnails
            var landscapeThumbnail = ProcessThumbnail(width, largeJpegLandscapePath);
            var thumbnailPortrait = ProcessThumbnail(width, largeJpegPortraitPath);
            var thumbnailSquare = ProcessThumbnail(width, largeJpegSquarePath);

            //Put the thumbnail paths into the viewbag
            ViewBag.LandscapeThumbnail = Path.Combine(projectFolder, landscapeThumbnail);
            ViewBag.PortraitThumbnail = Path.Combine(projectFolder, thumbnailPortrait);
            ViewBag.SquareThumbnail = Path.Combine(projectFolder, thumbnailSquare);

            //Need this for the link on the Index view
            ViewBag.ProjectFolder = projectFolder;
            ViewBag.OriginalLandscapeImage = $"{projectFolder}{landscapeThumbnail.Split('_').Last()}";
            ViewBag.OriginalPortraitImage = $"{projectFolder}{thumbnailPortrait.Split('_').Last()}";
            ViewBag.OriginalSquareImage = $"{projectFolder}{thumbnailSquare.Split('_').Last()}";

            return View();
        }

        /// <summary>
        /// Use the NuGet package ImageThumbnailCreator to create a thumbnail and return the path to the thumbnail.
        /// </summary>
        /// <param name="width"></param>
        /// <param name="fullImagePath"></param>
        /// <returns></returns>
        public static string ProcessThumbnail(float width, string fullImagePath)
        {
            return _thumbnailer.Create(width, projectImageFolder, Path.Combine(projectImageFolder, fullImagePath)).Split('\\').Last();
        }
    }
}
