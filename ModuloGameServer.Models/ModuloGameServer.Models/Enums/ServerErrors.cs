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
        SIGNUP_PHONE_EXISTS
    };
}

