using System;


namespace ModuloGameServer.Models
{

    public enum SERVER_ERROR
    {
        SERVER_ERROR,
        ACCESS_ERROR,
        
        BAD_DATA,

        SIGNUP_ALREADY_BOUND,
        SIGNUP_EMAIL_EXISTS,
        SIGNUP_BAD_EMAIL,
        SIGNUP_BAD_NICKNAME,
        SIGNUP_PHONE_EXISTS,

        SIGNIN_USER_NOT_FOUND,
        SIGNIN_ALREADY_BOUND,
        SIGNIN_USER_BLOCKED,
        SIGNIN_BADCODE,
        SIGNIN_EXPIRED,
        SIGNIN_BADUSER,
        SIGNIN_TOO_QUICK,

        USER_NOT_FOUND,
        
        BOT_DENIED,
        ANONIM_DENIED
    };
}

