using System;
using System.Collections.Generic;
using System.Linq;
using Moq;
using NUnit.Framework;
using TestNinja.Mocking;

namespace TestNinja.UnitTests.Mocking
{
    [TestFixture]
    public class HousekeeperService_Tests
    {
        private HousekeeperService _service;
        private Mock<IStatementGenerator> _statementGenerator;
        private Mock<IEmailSender> _emailSender;
        private Mock<HousekeeperService.IXtraMessageBox> _messageBox;

        private DateTime _statementDate = new DateTime(2019, 1, 1);

        private HousekeeperService.Housekeeper _houseKeeper;

        private string _statementFileName;

        [SetUp]
        public void SetUp()
        {
            _houseKeeper = new HousekeeperService.Housekeeper
            {
                Email = "1@1.com",
                Oid = 1,
                FullName = "aa",
                StatementEmailBody = "1@1.net"
            };

            var unitOfWork = new Mock<IUnitOfWork>();
            unitOfWork.Setup(uow => uow.Query<HousekeeperService.Housekeeper>()).Returns(new List<HousekeeperService.Housekeeper>
            {
                _houseKeeper
            }.AsQueryable());

            _statementFileName = "filename";
            _statementGenerator = new Mock<IStatementGenerator>();
            _statementGenerator.Setup(sg => sg.SaveStatement(_houseKeeper.Oid,
                                                             _houseKeeper.FullName,
                                                             _statementDate))
                               .Returns(() => _statementFileName);

            _emailSender = new Mock<IEmailSender>();
            _messageBox = new Mock<HousekeeperService.IXtraMessageBox>();

            _service = new HousekeeperService(unitOfWork.Object,
                                                 _statementGenerator.Object,
                                                 _emailSender.Object,
                                                 _messageBox.Object);
        }

        [Test]
        public void SendStatementEmails_WhenCalled_GenerateStatements()
        {
            _service.SendStatementEmails(_statementDate);

            _statementGenerator.Verify(sg => sg.SaveStatement(_houseKeeper.Oid,
                                                              _houseKeeper.FullName,
                                                              _statementDate));
        }

        [Test]
        public void SendStatementEmails_HousekeepersEmailIsNull_NotGenerateStatements()
        {
            _houseKeeper.Email = null;

            _service.SendStatementEmails(_statementDate);

            _statementGenerator.Verify(sg => sg.SaveStatement(_houseKeeper.Oid,
                                                              _houseKeeper.FullName,
                                                              _statementDate),
                                                              Times.Never);
        }

        [Test]
        public void SendStatementEmails_HousekeepersEmailIsWhiteSpace_NotGenerateStatements()
        {
            _houseKeeper.Email = " ";

            _service.SendStatementEmails(_statementDate);

            _statementGenerator.Verify(sg => sg.SaveStatement(_houseKeeper.Oid,
                                                              _houseKeeper.FullName,
                                                              _statementDate),
                                                              Times.Never);
        }

        [Test]
        public void SendStatementEmails_HousekeepersEmailIsEmpty_NotGenerateStatements()
        {
            _houseKeeper.Email = " ";

            _service.SendStatementEmails(_statementDate);

            _statementGenerator.Verify(sg => sg.SaveStatement(_houseKeeper.Oid,
                                                              _houseKeeper.FullName,
                                                              _statementDate),
                                                              Times.Never);
        }

        [Test]
        public void SendStatementEmails_WhenCalled_EmailTheStatement()
        {
            _service.SendStatementEmails(_statementDate);

            VerifyEmailSent();
        }

        [Test]
        public void SendStatementEmails_StatementFilenameIsNull_NotSendStatementEmail()
        {
            _statementFileName = null;

            _service.SendStatementEmails(_statementDate);

            VerifyEmailNotSent();
        }

        [Test]
        public void SendStatementEmails_StatementFilenameIsEmptyString_NotSendStatementEmail()
        {
            _statementFileName = "";

            _service.SendStatementEmails(_statementDate);

            VerifyEmailNotSent();
        }

        [Test]
        public void SendStatementEmails_StatementFilenameIsWhiteSpace_NotSendStatementEmail()
        {
            _statementFileName = " ";

            _service.SendStatementEmails(_statementDate);

            VerifyEmailNotSent();
        }

        [Test]
        public void SendStatementEmails_EmailSendingFails_DisplayMessageBox()
        {
            _emailSender.Setup(es => es.EmailFile(It.IsAny<String>(),
                                                  It.IsAny<String>(),
                                                  It.IsAny<String>(),
                                                  It.IsAny<String>())).Throws<Exception>();

            _service.SendStatementEmails(_statementDate);

            VerifyMessageBoxDisplay();
        }

        private void VerifyEmailNotSent()
        {
            _emailSender.Verify(es => es.EmailFile(It.IsAny<String>(),
                                                    It.IsAny<String>(),
                                                    It.IsAny<String>(),
                                                    It.IsAny<String>()),
                                                    Times.Never);
        }

        private void VerifyEmailSent()
        {
            _emailSender.Verify(es => es.EmailFile(_houseKeeper.Email,
                                                    _houseKeeper.StatementEmailBody,
                                                    _statementFileName,
                                                    It.IsAny<String>()));
        }

        private void VerifyMessageBoxDisplay()
        {
            _messageBox.Verify(mb => mb.Show(It.IsAny<String>(),
                                             It.IsAny<String>(),
                                             HousekeeperService.MessageBoxButtons.OK));
        }
    }
}