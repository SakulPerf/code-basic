using System;
using Xunit;
using FluentAssertions;

namespace CodeBasic.Tests
{
    public class PokdengTests
    {
        private const string Club = "Club";
        private const string Diamon = "Diamon";

        [Theory(DisplayName = "แต้มผู้เล่นชนะเจ้ามือ ผู้เล่นได้รับเงินเพิ่มเท่ากับเงินที่ลงพนัน")]
        [InlineData(100, 1, 1, Club, Diamon, 1, 2, Club, Diamon, 1000, 1100)]
        public void PlayerWinThenGainX1FromBet(int bet, int p1cn1, int p1cn2, string p1cs1, string p1cs2, int p2cn1, int p2cn2, string p2cs1, string p2cs2, int balance, int expectedBalance)
        {
            var sut = new Pokdeng { PlayerBalance = balance };
            sut.PlayerBalance = balance;
            sut.CheckGameResult(bet, p1cn1, p1cn2, 0, p1cs1, p1cs2, string.Empty, p2cn1, p2cn2, 0, p2cs1, p2cs2, string.Empty);
            Assert.Equal(expectedBalance, sut.PlayerBalance);
        }

        [Theory(DisplayName = "แต้มผู้เล่นแพ้เจ้ามือ ผู้เล่นเสียเงินเท่ากับเงินที่ลงพนัน")]
        [InlineData(100, 1, 2, Club, Diamon, 1, 1, Club, Diamon, 1000, 900)]
        public void PlayerLoseThenLoseX1FromBet(int bet, int p1cn1, int p1cn2, string p1cs1, string p1cs2, int p2cn1, int p2cn2, string p2cs1, string p2cs2, int balance, int expectedBalance)
        {
            var sut = new Pokdeng { PlayerBalance = balance };
            sut.PlayerBalance = balance;
            sut.CheckGameResult(bet, p1cn1, p1cn2, 0, p1cs1, p1cs2, string.Empty, p2cn1, p2cn2, 0, p2cs1, p2cs2, string.Empty);
            Assert.Equal(expectedBalance, sut.PlayerBalance);
        }

        [Theory(DisplayName = "แต้มผู้เล่นเสมอกับเจ้ามือ ผู้เล่นไม่เสียเงิน")]
        [InlineData(100, 1, 2, Club, Diamon, 1, 2, Club, Diamon, 1000, 1000)]
        public void PlayerDrawThenDoNothing(int bet, int p1cn1, int p1cn2, string p1cs1, string p1cs2, int p2cn1, int p2cn2, string p2cs1, string p2cs2, int balance, int expectedBalance)
        {
            var sut = new Pokdeng { PlayerBalance = balance };
            sut.PlayerBalance = balance;
            sut.CheckGameResult(bet, p1cn1, p1cn2, 0, p1cs1, p1cs2, string.Empty, p2cn1, p2cn2, 0, p2cs1, p2cs2, string.Empty);
            Assert.Equal(expectedBalance, sut.PlayerBalance);
        }

        [Theory(DisplayName = "แต้มผู้เล่นชนะเจ้ามือ โดยผลรวมเกิน 9 ผู้เล่นได้รับเงินเพิ่มเท่ากับเงินที่ลงพนัน")]
        [InlineData(100, 1, 1, Club, Diamon, 10, 3, Club, Diamon, 1000, 1100)]
        public void PlayerWinSumMoreThan9ThenGainX1FromBet(int bet, int p1cn1, int p1cn2, string p1cs1, string p1cs2, int p2cn1, int p2cn2, string p2cs1, string p2cs2, int balance, int expectedBalance)
        {
            var sut = new Pokdeng { PlayerBalance = balance };
            sut.PlayerBalance = balance;
            sut.CheckGameResult(bet, p1cn1, p1cn2, 0, p1cs1, p1cs2, string.Empty, p2cn1, p2cn2, 0, p2cs1, p2cs2, string.Empty);
            Assert.Equal(expectedBalance, sut.PlayerBalance);
        }

        [Theory(DisplayName = "แต้มผู้เล่นแพ้เจ้ามือ โดยผลรวมเกิน 9 ผู้เล่นเสียเงินเท่ากับเงินที่ลงพนัน")]
        [InlineData(100, 10, 3, Club, Diamon, 1, 1, Club, Diamon, 1000, 900)]
        public void PlayerLoseSumMoreThan9ThenLoseX1FromBet(int bet, int p1cn1, int p1cn2, string p1cs1, string p1cs2, int p2cn1, int p2cn2, string p2cs1, string p2cs2, int balance, int expectedBalance)
        {
            var sut = new Pokdeng { PlayerBalance = balance };
            sut.PlayerBalance = balance;
            sut.CheckGameResult(bet, p1cn1, p1cn2, 0, p1cs1, p1cs2, string.Empty, p2cn1, p2cn2, 0, p2cs1, p2cs2, string.Empty);
            Assert.Equal(expectedBalance, sut.PlayerBalance);
        }

        [Theory(DisplayName = "แต้มผู้เล่นเสมอกับเจ้ามือ โดยผลรวมเกิน 9 ผู้เล่นไม่เสียเงิน")]
        [InlineData(100, 10, 2, Club, Diamon, 10, 2, Club, Diamon, 1000, 1000)]
        public void PlayerDrawSumMoreThan9ThenDoNothing(int bet, int p1cn1, int p1cn2, string p1cs1, string p1cs2, int p2cn1, int p2cn2, string p2cs1, string p2cs2, int balance, int expectedBalance)
        {
            var sut = new Pokdeng { PlayerBalance = balance };
            sut.PlayerBalance = balance;
            sut.CheckGameResult(bet, p1cn1, p1cn2, 0, p1cs1, p1cs2, string.Empty, p2cn1, p2cn2, 0, p2cs1, p2cs2, string.Empty);
            Assert.Equal(expectedBalance, sut.PlayerBalance);
        }

        [Theory(DisplayName = "แต้มผู้เล่นชนะเจ้ามือโดยเป็นไพ่ป๊อก ผู้เล่นได้รับเงินเพิ่มเท่ากับเงินที่ลงพนัน")]
        [InlineData(100, 1, 1, Club, Diamon, 1, 8, Club, Diamon, 1000, 1100)]
        [InlineData(100, 1, 1, Club, Diamon, 8, 1, Club, Diamon, 1000, 1100)]
        [InlineData(100, 1, 1, Club, Diamon, 1, 7, Club, Diamon, 1000, 1100)]
        [InlineData(100, 1, 1, Club, Diamon, 7, 1, Club, Diamon, 1000, 1100)]
        public void PlayerWinPokThenGainX1FromBet(int bet, int p1cn1, int p1cn2, string p1cs1, string p1cs2, int p2cn1, int p2cn2, string p2cs1, string p2cs2, int balance, int expectedBalance)
        {
            var sut = new Pokdeng { PlayerBalance = balance };
            sut.PlayerBalance = balance;
            sut.CheckGameResult(bet, p1cn1, p1cn2, 0, p1cs1, p1cs2, string.Empty, p2cn1, p2cn2, 0, p2cs1, p2cs2, string.Empty);
            Assert.Equal(expectedBalance, sut.PlayerBalance);
        }

        [Theory(DisplayName = "แต้มผู้เล่นแพ้เจ้ามือโดยเป็นไพ่ป๊อก ผู้เล่นเสียเงินเท่ากับเงินที่ลงพนัน")]
        [InlineData(100, 1, 8, Club, Diamon, 1, 1, Club, Diamon, 1000, 900)]
        [InlineData(100, 8, 1, Club, Diamon, 1, 1, Club, Diamon, 1000, 900)]
        [InlineData(100, 1, 7, Club, Diamon, 1, 1, Club, Diamon, 1000, 900)]
        [InlineData(100, 7, 1, Club, Diamon, 1, 1, Club, Diamon, 1000, 900)]
        public void PlayerLosePokThenLoseX1FromBet(int bet, int p1cn1, int p1cn2, string p1cs1, string p1cs2, int p2cn1, int p2cn2, string p2cs1, string p2cs2, int balance, int expectedBalance)
        {
            var sut = new Pokdeng { PlayerBalance = balance };
            sut.PlayerBalance = balance;
            sut.CheckGameResult(bet, p1cn1, p1cn2, 0, p1cs1, p1cs2, string.Empty, p2cn1, p2cn2, 0, p2cs1, p2cs2, string.Empty);
            Assert.Equal(expectedBalance, sut.PlayerBalance);
        }

        [Theory(DisplayName = "แต้มผู้เล่นเสมอกับเจ้ามือโดยเป็นไพ่ป๊อก ผู้เล่นไม่เสียเงิน")]
        [InlineData(100, 1, 8, Club, Diamon, 1, 8, Club, Diamon, 1000, 1000)]
        [InlineData(100, 1, 8, Club, Diamon, 8, 1, Club, Diamon, 1000, 1000)]
        [InlineData(100, 8, 1, Club, Diamon, 1, 8, Club, Diamon, 1000, 1000)]
        [InlineData(100, 8, 1, Club, Diamon, 8, 1, Club, Diamon, 1000, 1000)]
        [InlineData(100, 1, 7, Club, Diamon, 1, 7, Club, Diamon, 1000, 1000)]
        [InlineData(100, 1, 7, Club, Diamon, 7, 1, Club, Diamon, 1000, 1000)]
        [InlineData(100, 7, 1, Club, Diamon, 1, 7, Club, Diamon, 1000, 1000)]
        [InlineData(100, 7, 1, Club, Diamon, 7, 1, Club, Diamon, 1000, 1000)]
        public void PlayerDrawPokThenDoNothing(int bet, int p1cn1, int p1cn2, string p1cs1, string p1cs2, int p2cn1, int p2cn2, string p2cs1, string p2cs2, int balance, int expectedBalance)
        {
            var sut = new Pokdeng { PlayerBalance = balance };
            sut.PlayerBalance = balance;
            sut.CheckGameResult(bet, p1cn1, p1cn2, 0, p1cs1, p1cs2, string.Empty, p2cn1, p2cn2, 0, p2cs1, p2cs2, string.Empty);
            Assert.Equal(expectedBalance, sut.PlayerBalance);
        }

        [Theory(DisplayName = "ผู้เล่นลงเงินเกินที่ตัวเองมี ระบบแจ้งเตือนข้อผิดพลาด และยกเลิกการเล่นในรอบนั้นไป")]
        [InlineData(2000, 1, 1, Club, Diamon, 1, 2, Club, Diamon, 1000, 1000)]
        public void PlayerBetInvalidThenDoNothing(int bet, int p1cn1, int p1cn2, string p1cs1, string p1cs2, int p2cn1, int p2cn2, string p2cs1, string p2cs2, int balance, int expectedBalance)
        {
            var sut = new Pokdeng { PlayerBalance = balance };
            sut.PlayerBalance = balance;
            sut.CheckGameResult(bet, p1cn1, p1cn2, 0, p1cs1, p1cs2, string.Empty, p2cn1, p2cn2, 0, p2cs1, p2cs2, string.Empty);
            Assert.Equal(expectedBalance, sut.PlayerBalance);
        }

        [Theory(DisplayName = "แต้มผู้เล่นชนะเจ้ามือ ผู้เล่นได้รับเงินเพิ่มเท่ากับเงินที่ลงพนัน")]
        [InlineData(100, 1, 1, Club, Diamon, 2, 2, Club, Diamon, 1000, 1200)]
        [InlineData(100, 1, 1, Club, Diamon, 1, 2, Club, Club, 1000, 1200)]
        public void PlayerWinDoubleThenGainX1FromBet(int bet, int p1cn1, int p1cn2, string p1cs1, string p1cs2, int p2cn1, int p2cn2, string p2cs1, string p2cs2, int balance, int expectedBalance)
        {
            var sut = new Pokdeng { PlayerBalance = balance };
            sut.PlayerBalance = balance;
            sut.CheckGameResult(bet, p1cn1, p1cn2, 0, p1cs1, p1cs2, string.Empty, p2cn1, p2cn2, 0, p2cs1, p2cs2, string.Empty);
            Assert.Equal(expectedBalance, sut.PlayerBalance);
        }

        [Theory(DisplayName = "แต้มผู้เล่นแพ้เจ้ามือ แบบสองเด้ง ผู้เล่นเสียเงิน 2 เท่าของเงินที่ลงพนัน")]
        [InlineData(100, 2, 2, Club, Diamon, 1, 1, Club, Diamon, 1000, 800)]
        [InlineData(100, 1, 2, Club, Club, 1, 1, Club, Diamon, 1000, 800)]
        public void PlayerLoseDoubleThenLoseX1FromBet(int bet, int p1cn1, int p1cn2, string p1cs1, string p1cs2, int p2cn1, int p2cn2, string p2cs1, string p2cs2, int balance, int expectedBalance)
        {
            var sut = new Pokdeng { PlayerBalance = balance };
            sut.PlayerBalance = balance;
            sut.CheckGameResult(bet, p1cn1, p1cn2, 0, p1cs1, p1cs2, string.Empty, p2cn1, p2cn2, 0, p2cs1, p2cs2, string.Empty);
            Assert.Equal(expectedBalance, sut.PlayerBalance);
        }

        [Theory(DisplayName = "แต้มผู้เล่นเสมอกับเจ้ามือ แบบสองเด้ง ผู้เล่นไม่เสียเงิน")]
        [InlineData(100, 2, 2, Club, Club, 2, 2, Diamon, Diamon, 1000, 1000)]
        public void PlayerDrawDoubleThenDoNothing(int bet, int p1cn1, int p1cn2, string p1cs1, string p1cs2, int p2cn1, int p2cn2, string p2cs1, string p2cs2, int balance, int expectedBalance)
        {
            var sut = new Pokdeng { PlayerBalance = balance };
            sut.PlayerBalance = balance;
            sut.CheckGameResult(bet, p1cn1, p1cn2, 0, p1cs1, p1cs2, string.Empty, p2cn1, p2cn2, 0, p2cs1, p2cs2, string.Empty);
            Assert.Equal(expectedBalance, sut.PlayerBalance);
        }

        [Theory(DisplayName = "แต้มผู้เล่นชนะเจ้ามือ แบบสามเด้ง ผู้เล่นได้รับเงินเพิ่ม 3 เท่าของเงินที่ลงพนัน")]
        [InlineData(100, 1, 1, 1, Club, Diamon, Club, 2, 2, 1, Club, Club, Club, 1000, 1300)]
        public void PlayerWinTripleThenGainX1FromBet(int bet, int p1cn1, int p1cn2, int p1cn3, string p1cs1, string p1cs2, string p1cs3, int p2cn1, int p2cn2, int p2cn3, string p2cs1, string p2cs2, string p2cs3, int balance, int expectedBalance)
        {
            var sut = new Pokdeng { PlayerBalance = balance };
            sut.PlayerBalance = balance;
            sut.CheckGameResult(bet, p1cn1, p1cn2, p1cn3, p1cs1, p1cs2, p1cs3, p2cn1, p2cn2, p2cn3, p2cs1, p2cs2, p2cs3);
            Assert.Equal(expectedBalance, sut.PlayerBalance);
        }

        [Theory(DisplayName = "แต้มผู้เล่นแพ้เจ้ามือ แบบสามเด้ง ผู้เล่นเสียเงินเพิ่ม 3 เท่าของเงินที่ลงพนัน")]
        [InlineData(100, 2, 2, 1, Club, Club, Club, 1, 1, 1, Club, Diamon, Club, 1000, 700)]
        public void PlayerLoseTripleThenLoseX1FromBet(int bet, int p1cn1, int p1cn2, int p1cn3, string p1cs1, string p1cs2, string p1cs3, int p2cn1, int p2cn2, int p2cn3, string p2cs1, string p2cs2, string p2cs3, int balance, int expectedBalance)
        {
            var sut = new Pokdeng { PlayerBalance = balance };
            sut.PlayerBalance = balance;
            sut.CheckGameResult(bet, p1cn1, p1cn2, p1cn3, p1cs1, p1cs2, p1cs3, p2cn1, p2cn2, p2cn3, p2cs1, p2cs2, p2cs3);
            Assert.Equal(expectedBalance, sut.PlayerBalance);
        }

        [Theory(DisplayName = "แต้มผู้เล่นชนะเจ้ามือ แบบสามเด้ง ผู้เล่นได้รับเงินเพิ่ม 3 เท่าของเงินที่ลงพนัน")]
        [InlineData(100, 2, 2, 1, Club, Club, Club, 1, 1, 1, Diamon, Diamon, Diamon, 1000, 1000)]
        [InlineData(100, 2, 2, 1, Club, Club, Club, 3, 3, 3, Diamon, Diamon, Diamon, 1000, 1000)]
        public void PlayerDrawTripleThenDoNothing(int bet, int p1cn1, int p1cn2, int p1cn3, string p1cs1, string p1cs2, string p1cs3, int p2cn1, int p2cn2, int p2cn3, string p2cs1, string p2cs2, string p2cs3, int balance, int expectedBalance)
        {
            var sut = new Pokdeng { PlayerBalance = balance };
            sut.PlayerBalance = balance;
            sut.CheckGameResult(bet, p1cn1, p1cn2, p1cn3, p1cs1, p1cs2, p1cs3, p2cn1, p2cn2, p2cn3, p2cs1, p2cs2, p2cs3);
            Assert.Equal(expectedBalance, sut.PlayerBalance);
        }

        [Theory(DisplayName = "แต้มผู้เล่นชนะเจ้ามือโดยเป็นไพ่ป๊อก ผู้เล่นได้รับเงินเพิ่มเท่ากับเงินที่ลงพนันน")]
        [InlineData(100, 1, 1, 2, Club, Diamon, Diamon, 1, 8, 0, Club, Diamon, "", 1000, 1100)]
        [InlineData(100, 1, 7, 1, Club, Diamon, Diamon, 1, 8, 0, Club, Diamon, "", 1000, 1100)]
        [InlineData(100, 1, 1, 2, Club, Diamon, Diamon, 1, 7, 0, Club, Diamon, "", 1000, 1100)]
        [InlineData(100, 1, 7, 1, Club, Diamon, Diamon, 1, 7, 0, Club, Diamon, "", 1000, 1100)]
        public void PlayerWinPokWhenFaceTo3CardsThenGainX1FromBet(int bet, int p1cn1, int p1cn2, int p1cn3, string p1cs1, string p1cs2, string p1cs3, int p2cn1, int p2cn2, int p2cn3, string p2cs1, string p2cs2, string p2cs3, int balance, int expectedBalance)
        {
            var sut = new Pokdeng { PlayerBalance = balance };
            sut.PlayerBalance = balance;
            sut.CheckGameResult(bet, p1cn1, p1cn2, p1cn3, p1cs1, p1cs2, p1cs3, p2cn1, p2cn2, p2cn3, p2cs1, p2cs2, p2cs3);
            Assert.Equal(expectedBalance, sut.PlayerBalance);
        }

        [Theory(DisplayName = "แต้มผู้เล่นแพ้เจ้ามือโดยเป็นไพ่ป๊อก ผู้เล่นเสียเท่ากับเงินที่ลงพนัน")]
        [InlineData(100, 1, 8, 0, Club, Diamon, "", 1, 1, 2, Club, Diamon, Diamon, 1000, 900)]
        [InlineData(100, 1, 8, 0, Club, Diamon, "", 1, 7, 1, Club, Diamon, Diamon, 1000, 900)]
        [InlineData(100, 1, 7, 0, Club, Diamon, "", 1, 1, 2, Club, Diamon, Diamon, 1000, 900)]
        [InlineData(100, 1, 7, 0, Club, Diamon, "", 1, 7, 1, Club, Diamon, Diamon, 1000, 900)]
        public void PlayerlostPokWhenFaceTo3CardsThenLostX1FromBet(int bet, int p1cn1, int p1cn2, int p1cn3, string p1cs1, string p1cs2, string p1cs3, int p2cn1, int p2cn2, int p2cn3, string p2cs1, string p2cs2, string p2cs3, int balance, int expectedBalance)
        {
            var sut = new Pokdeng { PlayerBalance = balance };
            sut.PlayerBalance = balance;
            sut.CheckGameResult(bet, p1cn1, p1cn2, p1cn3, p1cs1, p1cs2, p1cs3, p2cn1, p2cn2, p2cn3, p2cs1, p2cs2, p2cs3);
            Assert.Equal(expectedBalance, sut.PlayerBalance);
        }

        

        /*
         * Normal cases
         * แต้มผู้เล่นชนะเจ้ามือ โดยผลรวมเกิน 9 ผู้เล่นได้รับเงินเพิ่มเท่ากับเงินที่ลงพนัน
         * แต้มผู้เล่นแพ้เจ้ามือ โดยผลรวมเกิน 9 ผู้เล่นเสียเงินเท่ากับเงินที่ลงพนัน
         * แต้มผู้เล่นเสมอกับเจ้ามือ โดยผลรวมเกิน 9 ผู้เล่นไม่เสียเงิน
         * ---
         * แต้มผู้เล่นชนะเจ้ามือโดยเป็นไพ่ป๊อก ผู้เล่นได้รับเงินเพิ่มเท่ากับเงินที่ลงพนัน
         * แต้มผู้เล่นแพ้เจ้ามือโดยเป็นไพ่ป๊อก ผู้เล่นเสียเงินเท่ากับเงินที่ลงพนัน
         * แต้มผู้เล่นเสมอกับเจ้ามือโดยเป็นไพ่ป๊อก ผู้เล่นไม่เสียเงิน
         * ---
         * ผู้เล่นลงเงินเกินที่ตัวเองมี ระบบแจ้งเตือนข้อผิดพลาด และยกเลิกการเล่นในรอบนั้นไป
         * 
         * 
         * Alternative cases
         * แต้มผู้เล่นชนะเจ้ามือ แบบสองเด้ง ผู้เล่นได้รับเงินเพิ่ม 2 เท่าของเงินที่ลงพนัน
         * แต้มผู้เล่นแพ้เจ้ามือ แบบสองเด้ง ผู้เล่นเสียเงิน 2 เท่าของเงินที่ลงพนัน
         * แต้มผู้เล่นเสมอกับเจ้ามือ แบบสองเด้ง ผู้เล่นไม่เสียเงิน
         * ---
         * แต้มผู้เล่นชนะเจ้ามือ แบบป๊อกสองเด้ง ผู้เล่นได้รับเงินเพิ่ม 2 เท่าของเงินที่ลงพนัน
         * แต้มผู้เล่นแพ้เจ้ามือ แบบป๊อกสองเด้ง ผู้เล่นเสียเงิน 2 เท่าของเงินที่ลงพนัน
         * แต้มผู้เล่นเสมอกับเจ้ามือ แบบป๊อกสองเด้ง ผู้เล่นไม่เสียเงิน
         * ---
         * แต้มผู้เล่นชนะเจ้ามือ แบบสามเด้ง ผู้เล่นได้รับเงินเพิ่ม 3 เท่าของเงินที่ลงพนัน
         * แต้มผู้เล่นแพ้เจ้ามือ แบบสามเด้ง ผู้เล่นเสียเงิน 3 เท่าของเงินที่ลงพนัน
         * แต้มผู้เล่นเสมอกับเจ้ามือ แบบสามเด้ง ผู้เล่นไม่เสียเงิน
         * ---
         * แต้มผู้เล่นชนะเจ้ามือ แบบไพ่เรียง ผู้เล่นได้รับเงินเพิ่ม 3 เท่าของเงินที่ลงพนัน
         * แต้มผู้เล่นแพ้เจ้ามือ แบบไพ่เรียง ผู้เล่นเสียเงิน 3 เท่าของเงินที่ลงพนัน
         * แต้มผู้เล่นเสมอกับเจ้ามือ แบบไพ่เรียง ผู้เล่นไม่เสียเงิน
         * ---
         * แต้มผู้เล่นชนะเจ้ามือ แบบไพ่ผี ผู้เล่นได้รับเงินเพิ่ม 3 เท่าของเงินที่ลงพนัน
         * แต้มผู้เล่นแพ้เจ้ามือ แบบไพ่ผี ผู้เล่นเสียเงิน 3 เท่าของเงินที่ลงพนัน
         * แต้มผู้เล่นเสมอกับเจ้ามือ แบบไพ่ผี ผู้เล่นไม่เสียเงิน
         * ---
         * แต้มผู้เล่นชนะเจ้ามือ แบบไพ่ตอง ผู้เล่นได้รับเงินเพิ่ม 5 เท่าของเงินที่ลงพนัน
         * แต้มผู้เล่นแพ้เจ้ามือ แบบไพ่ตอง ผู้เล่นเสียเงิน 5 เท่าของเงินที่ลงพนัน
         * แต้มผู้เล่นเสมอกับเจ้ามือ แบบไพ่ตอง ผู้เล่นไม่เสียเงิน
         * ---
         * ผู้เล่นได้ไพ่ป๊อก เจ้ามือได้ไพ่เรียง ผู้เล่นได้รับเงินเพิ่มเท่ากับเงินที่ลงพนัน
         * ผู้เล่นได้ไพ่ป๊อก เจ้ามือได้ไพ่ผี ผู้เล่นได้รับเงินเพิ่มเท่ากับเงินที่ลงพนัน
         * ผู้เล่นได้ไพ่ป๊อก เจ้ามือได้ไพ่ตอง ผู้เล่นได้รับเงินเพิ่มเท่ากับเงินที่ลงพนัน
         * ผู้เล่นได้ไพ่ป๊อก เจ้ามือได้ไพ่สามเด้ง ผู้เล่นได้รับเงินเพิ่มเท่ากับเงินที่ลงพนัน
         * ---
         * ผู้เล่นได้ไพ่ป๊อกสองเด้ง เจ้ามือได้ไพ่เรียง ผู้เล่นได้รับเงินเพิ่ม 2 เท่าของเงินที่ลงพนัน
         * ผู้เล่นได้ไพ่ป๊อกสองเด้ง เจ้ามือได้ไพ่ผี ผู้เล่นได้รับเงินเพิ่ม 2 เท่าของเงินที่ลงพนัน
         * ผู้เล่นได้ไพ่ป๊อกสองเด้ง เจ้ามือได้ไพ่ตอง ผู้เล่นได้รับเงินเพิ่ม 2 เท่าของเงินที่ลงพนัน
         * ผู้เล่นได้ไพ่ป๊อกสองเด้ง เจ้ามือได้ไพ่สามเด้ง ผู้เล่นได้รับเงินเพิ่ม 2 เท่าของเงินที่ลงพนัน
         * ---
         * เจ้ามือได้ไพ่ป๊อก ผู้เล่นได้ไพ่เรียง ผู้เล่นเสียเงินเท่ากับเงินที่ลงพนัน
         * เจ้ามือได้ไพ่ป๊อก ผู้เล่นได้ไพ่ผี ผู้เล่นเสียเงินเท่ากับเงินที่ลงพนัน
         * เจ้ามือได้ไพ่ป๊อก ผู้เล่นได้ไพ่ตอง ผู้เล่นเสียเงินเท่ากับเงินที่ลงพนัน
         * เจ้ามือได้ไพ่ป๊อก ผู้เล่นได้ไพ่สามเด้ง ผู้เล่นเสียเงินเท่ากับเงินที่ลงพนัน
         * ---
         * เจ้ามือได้ไพ่ป๊อกสองเด้ง ผู้เล่นได้ไพ่เรียง ผู้เล่นเสียเงิน 2 เท่าของเงินที่ลงพนัน
         * เจ้ามือได้ไพ่ป๊อกสองเด้ง ผู้เล่นได้ไพ่ผี ผู้เล่นเสียเงิน 2 เท่าของเงินที่ลงพนัน
         * เจ้ามือได้ไพ่ป๊อกสองเด้ง ผู้เล่นได้ไพ่ตอง ผู้เล่นเสียเงิน 2 เท่าของเงินที่ลงพนัน
         * เจ้ามือได้ไพ่ป๊อกสองเด้ง ผู้เล่นได้ไพ่สามเด้ง ผู้เล่นเสียเงิน 2 เท่าของเงินที่ลงพนัน
         * ---
         * ผู้เล่นเสียเงินเกินที่ตัวเองมี ระบบแจ้งเตือนข้อผิดพลาด และยกเลิกการเล่นในรอบนั้นไป
         * ผู้เล่นและเจ้ามือได้ไพ่ตองทั้งคู่ ผู้เล่นไม่เสียเงิน
         * ผู้เล่นและเจ้ามือได้ไพ่ผีทั้งคู่ ผู้เล่นไม่เสียเงิน
         * ผู้เล่นและเจ้ามือได้ไพ่เรียงทั้งคู่ ผู้เล่นไม่เสียเงิน
         * 
         * Exception cases
         * ผู้เล่นมีเงินไม่พอจ่าย ระบบแจ้งเตือนข้อผิดพลาด และยกเลิกการเล่นในรอบนั้นไป
         * ผู้เล่นลงเงินไม่ถูกต้อง ระบบแจ้งเตือนข้อผิดพลาด และยกเลิกการเล่นในรอบนั้นไป
         * เจ้ามือได้ไพ่ป๊อก ผู้เล่นมีไพ่ 3 ใบ ระบบจะคำนวณแค่ไพ่ 2 ใบแรกของผู้เล่น
         * ได้รับไพ่ที่ไม่ถูกต้อง ระบบแจ้งเตือนข้อผิดพลาด และยกเลิกการเล่นในรอบนั้นไป
         */
    }
}
