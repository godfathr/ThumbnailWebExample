using ImageThumbnailCreator;
using System;
using System.IO;
using System.Linq;
using System.Web.Mvc;

namespace ThumbnailWebExample.Controllers
{
    public class HomeController : Controller
    {
        //Create an instance of the JpegThumbnailer from the "ImageThumbnailCreator" NuGet package
        private static JpegThumbnailer jpegThumbnailer = new JpegThumbnailer();

        //Define the path to the base path of your web project
        private static string projectImageFolder = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, @"Content\sampleImages\");
        
        //Define the folder in your project where the thumbnail will be placed
        private static string projectFolder = @"Content/sampleImages/";
        
        //File name(s) for the file you wish to create a thumbnail for
        private static string largeLandscape = @"largeLandscape.jpg";
        private static string largePortrait = @"largePortrait.jpg";
        private static string largeSquare = @"largeSquare.jpg";

        public ActionResult Index()
        {
            //Choose the width of your thumbnail. The height will be auto calculated to keep the aspect ratio intact
            float width = 300;

            //Get a thumbnail for a landscape image
            var landscapeThumbnail = jpegThumbnailer.Create(width, projectImageFolder, Path.Combine(projectImageFolder, largeLandscape));
            ViewBag.LandscapeThumbnail = Path.Combine(projectFolder, landscapeThumbnail.Split('\\').Last());

            //Get a thumbnail for a portrait image
            var thumbnailPortrait = jpegThumbnailer.Create(width, projectImageFolder, Path.Combine(projectImageFolder, largePortrait));
            ViewBag.PortraitThumbnail = Path.Combine(projectFolder, thumbnailPortrait.Split('\\').Last());

            //Get a thumbnail for a square image
            var thumbnailSquare = jpegThumbnailer.Create(width, projectImageFolder, Path.Combine(projectImageFolder, largeSquare));
            ViewBag.SquareThumbnail = Path.Combine(projectFolder, thumbnailSquare.Split('\\').Last());


            //Need this for the link on the Index view
            ViewBag.ProjectFolder = projectFolder;
            ViewBag.OriginalLandscapeImage = $"{projectFolder}{landscapeThumbnail.Split('_').Last()}";
            ViewBag.OriginalPortraitImage = $"{projectFolder}{thumbnailPortrait.Split('_').Last()}";
            ViewBag.OriginalSquareImage = $"{projectFolder}{thumbnailSquare.Split('_').Last()}";

            return View();
        }
    }
}