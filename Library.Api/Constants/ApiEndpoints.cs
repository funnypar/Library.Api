namespace Library.Api.Constants
{
    public static class ApiEndpoints
    {
        private const string ApiBase = "api";

        public static class V1
        {
            private const string VersionBase = $"{ApiBase}/v1";

            public static class Books
            {
                private const string BookBase = $"{VersionBase}/books";

                public const string GetAll = BookBase;

                public const string Get = $"{BookBase}/{{idOrSlug}}";

                public const string Create = BookBase;

                public const string Update = $"{BookBase}/{{id:guid}}";

                public const string Delete = $"{BookBase}/{{id:guid}}";


                public const string Rate = $"{BookBase}/{{id:guid}}/ratings";
                public const string DeleteRate = $"{BookBase}/{{id:guid}}/ratings";
            }

            public static class Ratings
            {
                private const string RateingBase = $"{VersionBase}/ratings";

                public const string GetUserRatings = $"{RateingBase}/me";
            }

            public static class Authors
            {
                private const string AuthorBase = $"{VersionBase}/authors";

                public const string GetAll = AuthorBase;

                public const string Get = $"{AuthorBase}/{{idOrSlug}}";

                public const string Create = AuthorBase;

                public const string Update = $"{AuthorBase}/{{id:guid}}";

                public const string Delete = $"{AuthorBase}/{{id:guid}}";
            }
            public static class Publishers
            {
                private const string PublisherBase = $"{VersionBase}/publishers";

                public const string GetAll = PublisherBase;

                public const string Get = $"{PublisherBase}/{{idOrSlug}}";

                public const string Create = PublisherBase;

                public const string Update = $"{PublisherBase}/{{id:guid}}";

                public const string Delete = $"{PublisherBase}/{{id:guid}}";
            }

            public static class BookTags
            {
                private const string BookTagsBase = $"{VersionBase}/booktags";

                public const string GetAll = BookTagsBase;

                public const string Get = $"{BookTagsBase}/{{idOrSlug}}";

                public const string Create = BookTagsBase;

                public const string Update = $"{BookTagsBase}/{{id:guid}}";

                public const string Delete = $"{BookTagsBase}/{{id:guid}}";
            }
            public static class Users
            {
                private const string UsersBase = $"{VersionBase}/users";

                public const string GetAll = UsersBase;

                public const string Get = $"{UsersBase}/{{idOrSlug}}";

                public const string Create = UsersBase;

                public const string Update = $"{UsersBase}/{{id:guid}}";

                public const string Delete = $"{UsersBase}/{{id:guid}}";
            }

            public static class Image
            {
                private const string ImagesBase = $"{ApiBase}/images";

                public const string GetAll = ImagesBase;

                public const string Get = $"{ImagesBase}/{{id:guid}}";

                public const string Create = ImagesBase;

                public const string Update = $"{ImagesBase}/{{id:guid}}";

                public const string Delete = $"{ImagesBase}/{{id:guid}}";
            }

            public static class Authenticate
            {
                private const string AuthenticateBase = $"{ApiBase}/authenticate";

                public const string Login = $"{AuthenticateBase}/login";
            }

        }
    }
}
