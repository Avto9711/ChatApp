using ChatApp.Bl.Dto;
using ChatApp.Bl.Validators.Generic;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Text;

namespace ChatApp.Bl.Validators
{
    public class ChatRoomValidator : BaseValidator<ChatRoomDto>
    {
        public ChatRoomValidator()
        {
            RuleFor(x=>x.ChatRoomCode)
                .NotEmpty()
                    .WithMessage("The chatroom code is required to save the chatroom");
        }
    }
}
