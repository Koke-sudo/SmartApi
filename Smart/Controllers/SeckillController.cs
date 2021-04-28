using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using SmartMedical.DAL;
using SmartMedical.Model;
using SmartMedical.BLL;

namespace SmartMedical.Controllers
{
    [ApiController]
    [Route("SmartMedical/Seckill")]
    public class SeckillController : Controller
    {
        DBHelper _sqlServerHelper;
        SmartMedicalBLL _bll;
        public SeckillController(DBHelper sqlServerHelper, SmartMedicalBLL bll)
        {
            _sqlServerHelper = sqlServerHelper;
            _bll = bll;
        }



        /// <summary>
        /// 秒杀管理显示、查询
        /// </summary>
        /// <param name="name"></param>
        /// <param name="id"></param>
        /// <param name="pageindex"></param>
        /// <param name="pagesize"></param>
        /// <returns></returns>
        [Route("InitDate"),HttpGet]
        public IActionResult InitDate(string name="",int id=-1,int pageindex = 1, int pagesize = 3)//秒杀列表显示
        {
            var list =_bll.list(name,id);
            int count = list.Count;
            int pagecount = (int)Math.Ceiling(count * 1.0 / pagesize);
            var _list = list.Skip((pageindex - 1) * pagesize).Take(pagesize).ToList();

            return Ok(new { data = _list ,
                pages = new
                {
                    pro = pageindex > 1 ? pageindex - 1 : 1,
                    next = pageindex < pagecount ? pageindex + 1 : pagecount,
                    last = pagecount
                }
            });
        }
        /// <summary>
        /// 秒杀管理查询商品列表
        /// </summary>
        /// <param name="state"></param>
        /// <param name="id"></param>
        /// <param name="name"></param>
        /// <param name="pageindex"></param>
        /// <param name="pagesize"></param>
        /// <returns></returns>
        [Route("initgood"),HttpGet]
        public IActionResult InitGood(int state = -1,int id=-1,string name="",int pageindex = 1, int pagesize = 3)//商品列表显示
        {
            var list = _bll.goods(state,id,name);
            int count = list.Count;
            int pagecount = (int)Math.Ceiling(count * 1.0 / pagesize);
            var _list = list.Skip((pageindex - 1) * pagesize).Take(pagesize).ToList();

            return Ok(new
            {
                data = _list,
                pages = new
                {
                    pro = pageindex > 1 ? pageindex - 1 : 1,
                    next = pageindex < pagecount ? pageindex + 1 : pagecount,
                    last = pagecount
                }
            });
        }

        /// <summary>
        /// 秒杀管理，秒杀修改
        /// </summary>
        /// <param name="s"></param>
        /// <returns></returns>
         [Route("seckillup"),HttpPost]
        public IActionResult SeckillUp(Seckill s)//秒杀修改
        {
            int h = _bll.SeckillUpdate(s);
            return Ok(new { msg = h > 0 ? "修改成功" : "修改失败" });
        }
        /// <summary>
        /// 秒杀管理，商品的删除
        /// </summary>
        /// <param name="s"></param>
        /// <returns></returns>
        [Route("del"), HttpPost]
        public IActionResult Del(int id)//商品的删除
        {
            int h = _bll.Delete(id);
            return Ok(new { msg = h > 0 ? "删除成功" : "删除失败",state=h>0?true:false });
        }
        /// <summary>
        /// 秒杀管理，商品的添加
        /// </summary>
        /// <param name="s"></param>
        /// <returns></returns>
        [Route("add"), HttpPost]
        public IActionResult Add(Seckill s)//商品的添加
        {
            int h = _bll.SeckillAdd(s);
            return Ok(new { msg = h > 0 ? "添加成功" : "添加失败" });
        }
        /// <summary>
         /// 秒杀管理，秒杀的删除
         /// </summary>
         /// <param name="s"></param>
         /// <returns></returns>
        [Route("seckilldel"), HttpPost]

        public IActionResult SeckillDel(int id)//秒杀的删除
        {
            int h = _bll.SeckillDel(id);
            return Ok(new { msg = h > 0 ? "删除成功" : "删除失败", state = h > 0 ? true : false });
        }
        /// <summary>
        /// 秒杀管理，修改商品的状态
        /// </summary>
        /// <param name="s"></param>
        /// <returns></returns>
        [Route("up"), HttpPost]
        public IActionResult Up(int goodseckill,int id)//修改商品的状态
        {
            int h = _bll.UpdateState(goodseckill,id);
            return Ok(new { msg = h > 0 ? "提交成功" : "提交失败" });
        }
        /// <summary>
        /// 秒杀管理，秒杀的修改
        /// </summary>
        /// <param name="s"></param>
        /// <returns></returns>
        [Route("seckillupdate"), HttpPost]
        public IActionResult SeckillUpdate(int id)//秒杀的修改
        {
            int h = _bll.SeckillUpdate(id);
            return Ok();
        }
        /// <summary>
        /// 秒杀管理，商品反填
        /// </summary>
        /// <param name="s"></param>
        /// <returns></returns>
        [Route("goodft"), HttpPost]
        public IActionResult GoodFt(int id)//商品反填
        {
            List<Goods> list = _bll.GoodFt(id);
            return Ok(new { data = list });
        }
        /// <summary>
        /// 秒杀管理，商品修改
        /// </summary>
        /// <param name="s"></param>
        /// <returns></returns>
        [Route("goodupdate"), HttpPost]
        public IActionResult GoodUpdate(Goods g)//商品修改
        {
            int h = _bll.GoodUpdate(g);
            return Ok(new { msg = h > 0 ? "提交成功" : "提交失败" });
        }
    }
}
