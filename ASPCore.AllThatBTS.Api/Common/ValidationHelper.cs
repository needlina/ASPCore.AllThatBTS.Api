using ASPCore.AllThatBTS.Api.Model;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ASPCore.AllThatBTS.Api.Common
{
    public class RequestBoardListMValidator : AbstractValidator<RequestBoardListM>
    {
        public RequestBoardListMValidator()
        {
            // Rules here
            RuleFor(x => x.PageNo)
            .NotEmpty()
            .GreaterThan(0)
            .WithMessage("페이지 번호 (PageNo)는 1보다 큰 값이어야 합니다.");

            // Rules here
            RuleFor(x => x.PageSize)
            .NotEmpty()
            .GreaterThan(0)
            .WithMessage("표시될 게시글 갯수 (PageSize)는 1보다 큰 값이어야 합니다.");

            // Rules here
            RuleFor(x => x.PageBlockSize)
            .NotEmpty()
            .GreaterThan(0)
            .WithMessage("페이지 블록 사이즈 (PageBlockSize)는 1보다 큰 값이어야 합니다.");

            // Rules here
            RuleFor(x => x.BoardId)
            .NotEmpty()
            .WithMessage("게시판 ID (BoardID)는 필수 값입니다.");

        }
    }

    public class WriteArticleMValidator : AbstractValidator<WriteArticleM>
    {
        public WriteArticleMValidator()
        {
            // Rules here
        }
    }

    public class ModifyArticleMValidator : AbstractValidator<ModifyArticleM>
    {
        public ModifyArticleMValidator()
        {
            // Rules here

        }
    }

    public class WriteCommentMValidator : AbstractValidator<WriteCommentM>
    {
        public WriteCommentMValidator()
        {
            // Rules here

        }
    }

    public class RequestCommentListMValidator : AbstractValidator<RequestCommentListM>
    {
        public RequestCommentListMValidator()
        {
            // Rules here

        }
    }

    public class RequestSubCommentListMValidator : AbstractValidator<RequestSubCommentListM>
    {
        public RequestSubCommentListMValidator()
        {
            // Rules here

        }
    }

    public class WriteSubCommentMValidator : AbstractValidator<WriteSubCommentM>
    {
        public WriteSubCommentMValidator()
        {
            // Rules here

        }
    }

    public class ModifyUserMValidator : AbstractValidator<ModifyUserM>
    {
        public ModifyUserMValidator()
        {
            // Rules here

        }
    }

    public class MakeUserMValidator : AbstractValidator<MakeUserM>
    {
        public MakeUserMValidator()
        {
            // Rules here

        }
    }

    public class RequestCralwerDataListMValidator : AbstractValidator<RequestCralwerDataListM>
    {
        public RequestCralwerDataListMValidator()
        {
            // Rules here

        }
    }

}
