using RESTCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace MovieShopREST
{
    public class TagModel
    {
        public long Id { get; set; }

        public string Name { get; set; }
    }

    public class TagEditModel
    {
        public string Name { get; set; }
    }

    public class TagsSource : BaseRESTSource
    {
        public SourcePage<TagModel> Get(int Page = 1, int PerPageCount = 20)
        {
            return new SourcePage<TagModel>
            {
                CurrentPage = 1,
                TotalPage = 1,
                TatalCount = 20,
                Items = new List<TagModel>
                {
                    new TagModel
                    {
                         Id = 1,
                         Name = "美剧",
                    },
                    new TagModel
                    {
                         Id = 2,
                         Name = "日剧",
                    },
                },
            };
        }

        public SourceItem<TagModel> Put(TagEditModel Model)
        {
            return new SourceItem<TagModel>
            {
                Item = new TagModel
                {
                    Id = 3,
                    Name = Model.Name,
                },
            };
        }
    }
}
