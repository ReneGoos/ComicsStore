using ComicsStore.Data.Model;
using ComicsStore.MiddleWare.Common;
using System;
using System.Collections.Generic;
using Xunit;

namespace ComicsStore.Tests
{
    public class EnumHelperTest
    {
        [Fact]
        public void EnumHelper_Flags()
        {
            //
            var artistType = ArtistType.inker | ArtistType.writer;

            //Act
            var result = EnumHelper<ArtistType>.GetDisplayValue(artistType);
            var result1 = EnumHelper<ArtistType>.GetDisplayValues(artistType);
            var result2 = EnumHelper<ArtistType>.GetValues(artistType);
            var result3 = EnumHelper<ArtistType>.GetNames(artistType);

            //Assert
            Assert.Equal("writer, inker", result);
        }


        [Fact]
        public void EnumHelper_FlagsValue()
        {
            //
            var artistType = ArtistType.inker | ArtistType.writer;
            var input = new List<string>() { "writer", "inker" };

            //Act
            var result = EnumHelper<ArtistType>.ParseFlags(input);

            //Assert
            Assert.Equal(artistType, result);
        }

        [Fact]
        public void EnumHelper_FlagsNames()
        {
            //
            var artistType = ArtistType.inker | ArtistType.writer;

            //Act
            var result = EnumHelper<ArtistType>.GetNames(artistType);

            //Assert
            Assert.NotEmpty(result);
            Assert.Equal(2, result.Count);
        }

        [Fact]
        public void EnumHelper_Names()
        {
            //
            var bookType = BookType.book;

            //Act
            var result = EnumHelper<BookType>.GetNames(bookType);

            //Assert
            Assert.NotEmpty(result);
            Assert.Equal(1, result.Count);
        }
    }
}
