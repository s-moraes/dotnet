using System;
using System.Collections.Generic;
using System.Linq;
using Moq;
using NUnit.Framework;
using TestNinja.Mocking;

namespace TestNinja.UnitTests.Mocking
{
    [TestFixture]
    public class BookingHelper_Tests
    {
        private Booking _existingBooking;
        private Mock<IBookingRepository> _repository;

        [SetUp]
        public void SetUp()
        {
            _existingBooking = new Booking{ Id = 1,
                             ArrivalDate = ArriveOn(2020, 1, 15),
                             DepartureDate = DepartOn(2020, 1, 20),
                             Reference = "a",
                             };

            _repository = new Mock<IBookingRepository>();
            _repository.Setup(r => r.getActiveBookings(1)).Returns(new List<Booking>{
                _existingBooking
            }.AsQueryable());
        }

        [Test]
        public void OverlappingBookingsExist_BookingStartsAndFinishBeforeExistingBook_ReturnEmptyString()
        {
            var result = BookingHelper.OverlappingBookingsExist(new Booking{ Id = 1,
                                    ArrivalDate = Before(_existingBooking.ArrivalDate, days: 2),
                                    DepartureDate = Before(_existingBooking.ArrivalDate),
            }, _repository.Object);

            Assert.That(result, Is.Empty);
        }

        [Test]
        public void OverlappingBookingsExist_BookingStartsBeforeAndFinishesInTheMiddleOfAnExistingBook_ReturnExtingBookings()
        {
            var result = BookingHelper.OverlappingBookingsExist(new Booking{ Id = 1,
                                    ArrivalDate = Before(_existingBooking.ArrivalDate),
                                    DepartureDate = After(_existingBooking.ArrivalDate),
            }, _repository.Object);

            Assert.That(result, Is.EqualTo(_existingBooking.Reference));
        }

        [Test]
        public void OverlappingBookingsExist_BookingStartsBeforeAndFinishesAfterOfAnExistingBook_ReturnExtingBookings()
        {
            var result = BookingHelper.OverlappingBookingsExist(new Booking{ Id = 1,
                                    ArrivalDate = Before(_existingBooking.ArrivalDate),
                                    DepartureDate = After(_existingBooking.DepartureDate),
            }, _repository.Object);

            Assert.That(result, Is.EqualTo(_existingBooking.Reference));
        }

        [Test] //-----------------
        public void OverlappingBookingsExist_BookingStartsAndFinishesInTheMiddleOfAnExistingBook_ReturnExtingBookings()
        {
            var result = BookingHelper.OverlappingBookingsExist(new Booking{ Id = 1,
                                    ArrivalDate = After(_existingBooking.ArrivalDate),
                                    DepartureDate = Before(_existingBooking.DepartureDate),
            }, _repository.Object);

            Assert.That(result, Is.EqualTo(_existingBooking.Reference));
        }

        [Test] //----------------
        public void OverlappingBookingsExist_BookingStartsInTheMiddleOfAnExistingBookAndFinishesAfter_ReturnExtingBookings()
        {
            var result = BookingHelper.OverlappingBookingsExist(new Booking{ Id = 1,
                                    ArrivalDate = After(_existingBooking.ArrivalDate),
                                    DepartureDate = After(_existingBooking.DepartureDate),
            }, _repository.Object);

            Assert.That(result, Is.EqualTo(_existingBooking.Reference));
        }

        [Test]
        public void OverlappingBookingsExist_BookingStartsAndFinishesAfter_ReturnEmptyString()
        {
            var result = BookingHelper.OverlappingBookingsExist(new Booking{ Id = 1,
                                    ArrivalDate = After(_existingBooking.DepartureDate),
                                    DepartureDate = After(_existingBooking.DepartureDate, 2),
            }, _repository.Object);

            Assert.That(result, Is.Empty);
        }

        [Test]
        public void BookingsOverlapButNewBookingIsCancelled_ReturnsEmptyString()
        {
            var result = BookingHelper.OverlappingBookingsExist(new Booking{ Id = 1,
                                    ArrivalDate = After(_existingBooking.ArrivalDate),
                                    DepartureDate = Before(_existingBooking.DepartureDate),
                                    Status = "Cancelled",
            }, _repository.Object);

            Assert.That(result, Is.Empty);
        }

        private DateTime After(DateTime dateTime, int days = 1)
        {
            return dateTime.AddDays(days);
        }
        private DateTime Before(DateTime dateTime, int days = 1)
        {
            return dateTime.AddDays(-days);
        }

        private DateTime ArriveOn(int year, int month, int day)
        {
            return new DateTime(year, month, day, 14, 00, 00);
        }
        
        private DateTime DepartOn(int year, int month, int day)
        {
            return new DateTime(year, month, day, 10, 00, 00);
        }
    }
}