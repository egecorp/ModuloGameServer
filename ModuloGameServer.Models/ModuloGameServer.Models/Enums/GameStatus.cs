using System;


namespace ModuloGameServer.Models
{

    public enum GAME_STATUS
    {
        // NO ROUNDS
        GAME_CREATING = 0,
        GAME_WAIT_USER1 = 1,
        GAME_WAIT_USER2 = 2,
        GAME_ACCEPT = 3,
        GAME_DECLINE_USER1 = 4,
        GAME_DECLINE_USER2 = 5,
        GAME_CANCEL = 6,
        GAME_TIMEOUT = 7,
        GAME_RANDOM_CREATING = 10,
        GAME_RANDOM_FOUND = 11,
        GAME_RANDOM_CANCEL = 12,
        GAME_RANDOM_TIMEOUT = 13,

        // 1 ROUND
        GAME_ROUND_1_NOUSER = 100,
        GAME_ROUND_1_USER1_DONE = 101,
        GAME_ROUND_1_USER2_DONE = 102,
        GAME_ROUND_1_USER1_GIVEUP = 103,
        GAME_ROUND_1_USER2_GIVEUP = 104,
        GAME_ROUND_1_TIMEOUT = 105,

        // 2 ROUND
        GAME_ROUND_2_NOUSER = 200,
        GAME_ROUND_2_USER1_DONE = 201,
        GAME_ROUND_2_USER2_DONE = 202,
        GAME_ROUND_2_USER1_GIVEUP = 203,
        GAME_ROUND_2_USER2_GIVEUP = 204,
        GAME_ROUND_2_TIMEOUT = 205,

        // 3 ROUND
        GAME_ROUND_3_NOUSER = 300,
        GAME_ROUND_3_USER1_DONE = 301,
        GAME_ROUND_3_USER2_DONE = 302,
        GAME_ROUND_3_USER1_GIVEUP = 303,
        GAME_ROUND_3_USER2_GIVEUP = 304,
        GAME_ROUND_3_TIMEOUT = 305,

        // 4 ROUND
        GAME_ROUND_4_NOUSER = 400,
        GAME_ROUND_4_USER1_DONE = 401,
        GAME_ROUND_4_USER2_DONE = 402,
        GAME_ROUND_4_USER1_GIVEUP = 403,
        GAME_ROUND_4_USER2_GIVEUP = 404,
        GAME_ROUND_4_TIMEOUT = 405,

        // 5 ROUND
        GAME_ROUND_5_NOUSER = 500,
        GAME_ROUND_5_USER1_DONE = 501,
        GAME_ROUND_5_USER2_DONE = 502,
        GAME_ROUND_5_USER1_GIVEUP = 503,
        GAME_ROUND_5_USER2_GIVEUP = 504,
        GAME_ROUND_5_TIMEOUT = 505,


        // FINISH
        GAME_FINISH_USER1_WIN = 1001,
        GAME_FINISH_USER2_WIN = 1002,
        GAME_FINISH_USER2_DRAW = 1003

    };
}

