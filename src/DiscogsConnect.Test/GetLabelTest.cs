﻿using FluentAssertions;
using System.Linq;
using Xunit;

namespace DiscogsConnect.Test
{  
    [Collection("DiscogsClient")]
    public class GetLabelTest
    {
        protected IDiscogsClient Client { get; private set; }

        public GetLabelTest(DiscogsClientFixture fixture)
        {
            Client = fixture.DiscogsClient;
        }

        [Fact]
        public void GetValidLabel_ExpectData()
        {
            // Act
            var response = Client.GetLabel(1).Result;

            // Assert
            response.Should().NotBeNull();

            response.Profile.Should().NotBeNullOrWhiteSpace();
            response.ReleasesUrl.Should().Be("https://api.discogs.com/labels/1/releases");
            response.Name.Should().Be("Planet E");
            response.ContactInfo.Should().NotBeNullOrWhiteSpace();
            response.Uri.Should().Be("https://www.discogs.com/label/1-Planet-E");
            response.Sublabels.Should().NotBeEmpty();
            response.Urls.Should().NotBeEmpty();
            response.Images.Should().NotBeEmpty();

            var sampleImage = response.Images.First();

            sampleImage.Uri.Should().NotBeNullOrWhiteSpace();
            sampleImage.Height.Should().BeGreaterThan(0);
            sampleImage.Width.Should().BeGreaterThan(0);
            sampleImage.ResourceUrl.Should().NotBeNullOrWhiteSpace();
            sampleImage.Type.Should().NotBeNull();
            sampleImage.Uri150.Should().NotBeNullOrEmpty();

            response.ResourceUrl.Should().Be("https://api.discogs.com/labels/1");
            response.Id.Should().Be(1);
            //response.DataQuality.Should().Be(DataQuality.Correct);
        }

        [Fact]
        public void GetValidLabelRelease_ExpectData()
        {
            // Act
            var response = Client.GetLabelRelease(1).Result;

            // Assert
            response.Should().NotBeNull();
            response.Items.Should().NotBeEmpty();

            response.Pagination.PerPage.Should().Be(50);
            response.Pagination.Items.Should().BeGreaterThan(300);
            response.Pagination.Page.Should().Be(1);
            response.Pagination.Pages.Should().BeGreaterThan(5);

            response.Items.Should().NotBeEmpty();
            response.Items.Should().HaveCount(50);

            var labelRelease = response.Items.SingleOrDefault(x => x.Id == 2801);
            labelRelease.Should().NotBeNull();
            labelRelease.Status.Should().Be("Accepted");
            labelRelease.Thumb.Should().NotBeNullOrWhiteSpace();
            labelRelease.Format.Should().Be("CD, Mixed");
            labelRelease.Title.Should().Be("DJ-Kicks");
            labelRelease.Catno.Should().Be("!K7071CD");
            labelRelease.ResourceUrl.Should().Be("https://api.discogs.com/releases/2801");
            labelRelease.Artist.Should().Be("Andrea Parker");
        }
    }
}