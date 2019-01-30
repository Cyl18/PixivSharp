using System;
using System.Collections.Generic;
using System.Text;

namespace PixivSharp.Objects
{
    public interface IPage<T> where T : IPage<T>
    {
        Uri NextUrl { get; }
        // TSource[] Datas { get; }
        // 这里我不这么写是因为有的东西(如推荐) 一直翻页会瞬间爆炸
    }
}
