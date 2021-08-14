using System;
using Flunt.Notifications;
using Flunt.Validations;
using PaymentContext.Domain.Commands;
using PaymentContext.Domain.Entities;
using PaymentContext.Domain.Enums;
using PaymentContext.Domain.Repositories;
using PaymentContext.Domain.Service;
using PaymentContext.Domain.ValueObjects;
using PaymentContext.Shared.Commands;
using PaymentContext.Shared.Handlers;

namespace PaymentContext.Domain.Handlers
{
    public class SubscriptionHandler :
                      Notifiable,
                      IHandler<CreateBoletoSubscriptionCommand>,
                      IHandler<CreatePayPalSubscriptionCommand>
    {
        private readonly IStudentRepository _repository;
        private readonly IEmailService _emailService;

        public SubscriptionHandler(IStudentRepository repository, IEmailService emailService)
        {
            _repository = repository;
            _emailService = emailService;
        }

        public ICommandResult Handler(CreateBoletoSubscriptionCommand command)
        {
            command.Validate();
            if (command.Invalid) {
                AddNotifications(command);
                return new CommandResult (false, "Subscription failed");
            }

            // verifica se documento esta cadastrado
            if (_repository.DocumentExists(command.Document))
            {
                AddNotification("Document", "Documento já em uso");
            }

            // email cadastrado ?
           if (_repository.EmailExists(command.PayerEmail))
            {
                AddNotification("Email", "Email já em uso");
            }

            // gerar vos
            var name = new Name(command.FirstName, command.LastName);
            var doc = new Document(command.PayerDocument, command.PayerDocumentType);
            var email = new Email(command.PayerEmail);
            var address = new Address(command.Street,
                                        command.Number,
                                        command.Neighborhood,
                                        command.City,
                                        command.Street,
                                        command.Country,
                                        command.ZipCode);

            // gerar entidades
            var student = new Student(name, doc, email);
            var subscription = new Subscription(DateTime.Now.AddMonths(1));
            var payment = new BoletoPayment(command.BarCode,
                                            command.BoletoNumber,
                                            command.PaidDate,
                                            command.ExpireDate,
                                            command.Total,
                                            command.TotalPaid,
                                            command.Payer,
                                            new Document (command.PayerDocument, command.PayerDocumentType),
                                            address,
                                            email);

            // Relacionamentos
            subscription.AddPayment(payment);
            student.AddSubscription(subscription);

            // Agrupar validacoes
            AddNotifications(name, doc, email, address, student, subscription, payment);

            // Checar notificacoes
            if (Invalid)
                return new CommandResult(false, "Não foi possivel realizar a subscription");

            // salva dados
            _repository.CreateSubscription(student);

            // enviar email de confirmacao
            _emailService.Send(student.Name.ToString(),
                                student.Email.Address,
                                "Bem vindo",
                                "Subscription created");

            // retorna
            return new CommandResult(true, "Subscription success");
        }

        public ICommandResult Handler(CreatePayPalSubscriptionCommand command)
        {
            return new CommandResult(false, "TO DO");
        }
    }
}