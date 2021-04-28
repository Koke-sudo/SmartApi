using Microsoft.AspNetCore.Mvc;
using System;
using SmartMedical.BLL;
using SmartMedical.Model;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Data;

namespace SmartMedical.Controllers
{
    [ApiController]
    [Route("SmartMedical/Commodity")]
    public class CommodityController : Controller
    {
        SmartMedicalBLL _bll;
        Microsoft.AspNetCore.Hosting.IWebHostEnvironment _host;
        public CommodityController(SmartMedicalBLL bll, Microsoft.AspNetCore.Hosting.IWebHostEnvironment host)
        {
            _bll = bll;
            _host = host;
        }
        /// <summary>
        /// 商品分类
        /// </summary>
        /// <param name="pageindex"></param>
        /// <param name="name"></param>
        /// <param name="id"></param>
        /// <returns></returns>
        [Route("commodityshow"),HttpGet]
        public IActionResult CommodityShow(int pageindex = 1, string name = "", int id = -1)
        {
            List<GType> list = _bll.GTypes(name, id);
            int pagesize = 5;

            var page = new PagesDto(pageindex, pagesize, list.Count);
            var _list = list.Skip((pageindex - 1) * pagesize).Take(pagesize);
            return Ok(new { data = _list, pages = page });
        }
        /// <summary>
        /// 商品分类反填
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [Route("commodtityream"), HttpPost]
        public IActionResult CommodtityReam(int id)
        {
            List<GType> list = _bll.GTypes(id);
            return Ok(new { data = list });
        }

        /// <summary>
        /// 商品订单和查询
        /// </summary>
        /// <param name="pageindex"></param>
        /// <param name="name"></param>
        /// <param name="id"></param>
        /// <param name="tid"></param>
        /// <param name="nid"></param>
        /// <returns></returns>
        [Route("getorders"), HttpGet]
        public IActionResult GetOrders(int pageindex = 1, string name = "", int id = -1, int tid = -1, int nid = -1)
        {

            List<Order> list = _bll.GetOrders(name, id, tid, nid);
            int pagesize = 5;

            var page = new PagesDto(pageindex, pagesize, list.Count);
            var _list = list.Skip((pageindex - 1) * pagesize).Take(pagesize);
            return Ok(new { data = _list, pages = page });
        }

        /// <summary>
        /// 商品列表显示
        /// </summary>
        /// <param name="pageindex"></param>
        /// <param name="name"></param>
        /// <param name="id"></param>
        /// <param name="tid"></param>
        /// <returns></returns>
        [Route("getgoods"), HttpGet]
        public IActionResult GetGoods(int pageindex = 1, string name = "", int id = -1, int tid = -1)
        {
            List<Goods> list = _bll.GetGoods(name, id, tid);
            int pagesize = 5;

            var page = new PagesDto(pageindex, pagesize, list.Count);
            var _list = list.Skip((pageindex - 1) * pagesize).Take(pagesize);
            return Ok(new { data = _list, pages = page });
        }
        /// <summary>
        /// 修改商品列表上架操作
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [Route("goodshelves"),HttpPost]
        public IActionResult GoodShelves(int id)
        {
            int h = _bll.GoodShelves(id);
            return Ok(new { count = h, msg = h > 0 ? "操作成功!" : "操作失败!" });
        }
        /// <summary>
        /// 修改商品列表下架操作
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [Route("goodshelf"), HttpPost]
        public IActionResult GoodShelf(int id)
        {
            int h = _bll.GoodShelf(id);
            return Ok(new { count = h, msg = h > 0 ? "操作成功!" : "操作失败!" });
        }
        /// <summary>
        /// 商品列表数据反填
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [Route("goodreverse"),HttpPost]
        public IActionResult GoodReverse(int id)
        {
            List<Goods> list = _bll.GoodReverse(id);
            return Ok(new { data = list });
        }
        /// <summary>
        /// 商品列表的修改操作
        /// </summary>
        /// <param name="s"></param>
        /// <returns></returns>
        [Route("goodmodify"),HttpPost]
        public IActionResult GoodModify(Goods s)
        {
            int h = _bll.GoodUpd(s);
            return Ok(new { count = h });
        }
        [Route("Pinsert"),HttpGet]
        public IActionResult Pinsert()
        {
            var files = Request.Form.Files;

            var path = _host.WebRootPath + "/pinsert/";

            if (!System.IO.Directory.Exists(path))
            {
                System.IO.Directory.CreateDirectory(path);
            }
            var name = "";
            foreach (var item in files)
            {
                name = item.FileName;

                name = Guid.NewGuid().ToString("N") + System.IO.Path.GetExtension(item.FileName);
                using (var stream = new System.IO.FileStream($"{path}{name}", System.IO.FileMode.Create, System.IO.FileAccess.ReadWrite))
                {
                    item.CopyTo(stream);
                }

            }

            return Ok(new { filepath = "/pinsert/" + name });
        }


    }
}
