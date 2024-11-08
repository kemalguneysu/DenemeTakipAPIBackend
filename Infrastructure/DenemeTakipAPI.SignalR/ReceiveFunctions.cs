using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DenemeTakipAPI.SignalR
{
    public static class ReceiveFunctions
    {
        public const string AytAddedMessage = "receiveAytAddedMessage";
        public const string AytUpdatedMessage = "receiveAytUpdatedMessage";
        public const string AytDeletedMessage = "receiveAytDeletedMessage";


        public const string TytAddedMessage = "receiveTytAddedMessage";
        public const string TytUpdatedMessage = "receiveTytUpdatedMessage";
        public const string TytDeletedMessage = "receiveTytDeletedMessage";

        public const string KonuAddedMessage = "receiveKonuAddedMessage";
        public const string KonuUpdatedMessage = "receiveKonuUpdatedMessage";
        public const string KonuDeletedMessage = "receiveKonuDeletedMessage";

        public const string DersAddedMessage = "receiveDersAddedMessage";
        public const string DersUpdatedMessage = "receiveDersUpdatedMessage";
        public const string DersDeletedMessage = "receiveDersDeletedMessage";

        public const string UserUpdatedMessage = "receiveUserUpdatedMessage";

        public const string UserKonuAddedMessage = "receiveUserKonuAddedMessage";
        public const string UserKonuUpdatedMessage = "receiveUserKonuUpdatedMessage";
        public const string UserKonuDeletedMessage = "receiveUserKonuDeletedMessage";

        public const string ToDoElementAddedMessage = "receiveToDoElementAddedMessage";
        public const string ToDoElementUpdatedMessage = "receiveToDoElementUpdatedMessage";
        public const string ToDoElementDeletedMessage = "receiveToDoElementDeletedMessage";


    }
}
