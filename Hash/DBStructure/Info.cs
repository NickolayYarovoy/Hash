using Microsoft.VisualBasic;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Hash;
using System.Data;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace Hash.DBStructure 
{
    /// <summary>
    /// Class with data about flight
    /// </summary>
    public class BaseInfo
    {
        /// <value>
        /// Service field for interaction with the database
        /// </value>
        [Key]
        public int InfoId { get; set; }
        /// <value>
        /// Number of flight
        /// </value>
        public int FlightNumber { get; set; }
        /// <value>
        /// Aviacompany name
        /// </value>
        public string AviacompanyName { get; set; }
        /// <value>
        /// Arrival date
        /// </value>
        public DateTime ArrivalDate { get; set; }
        /// <value>
        /// Passenger number
        /// </value>
        public int PassengerNumber { get; set; }

        public BaseInfo(int InfoId, int FlightNumber, string AviacompanyName, DateTime ArrivalDate, int PassengerNumber)
        {
            this.InfoId = InfoId;
            this.FlightNumber = FlightNumber;
            this.AviacompanyName = AviacompanyName;
            this.ArrivalDate = ArrivalDate;
            this.PassengerNumber = PassengerNumber;
        }

        public BaseInfo(BaseInfo info)
        {
            this.InfoId = info.InfoId;
            this.FlightNumber = info.FlightNumber;
            this.AviacompanyName = info.AviacompanyName;
            this.ArrivalDate = info.ArrivalDate;
            this.PassengerNumber = info.PassengerNumber;
        }

        /// <summary>
        /// Checks whether the left element is less than the right one or not
        /// </summary>
        /// <returns>
        /// Returns true if left element is less, false else
        /// </returns>
        /// <param name="left">
        /// Left element
        /// </param>
        /// /// <param name="right">
        /// Right element
        /// </param>
        public static bool operator<(BaseInfo left, BaseInfo right)
        {
            if (left.ArrivalDate == right.ArrivalDate)
            {
                if (left.AviacompanyName == right.AviacompanyName)
                {
                    if (left.PassengerNumber == right.PassengerNumber)
                        return false;
                    else if (left.PassengerNumber > right.PassengerNumber)
                        return true;
                    return false;
                }
                else if (string.Compare(left.AviacompanyName, right.AviacompanyName) < 0)
                    return true;
                return false;
            }
            else if (left.ArrivalDate < right.ArrivalDate)
                return true;

            return false;
        }

        /// <summary>
        /// Checks whether the left element is greater than the right one or not
        /// </summary>
        /// <returns>
        /// Returns true if left element is greater, false else
        /// </returns>
        /// <param name="left">
        /// Left element
        /// </param>
        /// /// <param name="right">
        /// Right element
        /// </param>
        public static bool operator >(BaseInfo left, BaseInfo right)
        {
            if (left.ArrivalDate == right.ArrivalDate)
            {
                if (left.AviacompanyName == right.AviacompanyName)
                {
                    if (left.PassengerNumber == right.PassengerNumber)
                        return false;
                    else if (left.PassengerNumber < right.PassengerNumber)
                        return true;
                    return false;
                }
                else if (string.Compare(left.AviacompanyName, right.AviacompanyName) > 0)
                    return true;
                return false;
            }
            else if (left.ArrivalDate > right.ArrivalDate)
                return true;

            return false;
        }

        /// <summary>
        /// Checks whether the left element is less or equal than the right one or not
        /// </summary>
        /// <returns>
        /// Returns true if left element is less or equal, false else
        /// </returns>
        /// <param name="left">
        /// Left element
        /// </param>
        /// /// <param name="right">
        /// Right element
        /// </param>
        public static bool operator <=(BaseInfo left, BaseInfo right)
        {
            if (left.ArrivalDate == right.ArrivalDate)
            {
                if (left.AviacompanyName == right.AviacompanyName)
                {
                    if (left.PassengerNumber == right.PassengerNumber)
                        return true;
                    else if (left.PassengerNumber > right.PassengerNumber)
                        return true;
                    return false;
                }
                else if (string.Compare(left.AviacompanyName, right.AviacompanyName) < 0)
                    return true;
                return false;
            }
            else if (left.ArrivalDate < right.ArrivalDate)
                return true;

            return false;
        }

        /// <summary>
        /// Checks whether the left element is greater or equal than the right one or not
        /// </summary>
        /// <returns>
        /// Returns true if left element is greater or equal, false else
        /// </returns>
        /// <param name="left">
        /// Left element
        /// </param>
        /// /// <param name="right">
        /// Right element
        /// </param>
        public static bool operator >=(BaseInfo left, BaseInfo right)
        {
            if (left.ArrivalDate == right.ArrivalDate)
            {
                if (left.AviacompanyName == right.AviacompanyName)
                {
                    if (left.PassengerNumber == right.PassengerNumber)
                        return true;
                    else if (left.PassengerNumber < right.PassengerNumber)
                        return true;
                    return false;
                }
                else if (string.Compare(left.AviacompanyName, right.AviacompanyName) > 0)
                    return true;
                return false;
            }
            else if (left.ArrivalDate > right.ArrivalDate)
                return true;

            return false;
        }
    }

    public class InfoGood : BaseInfo
    {
        public UInt32 Hash;

        public InfoGood(int InfoId, int FlightNumber, string AviacompanyName, DateTime ArrivalDate, int PassengerNumber) : base(InfoId, FlightNumber, AviacompanyName, ArrivalDate, PassengerNumber)
        {
            this.Hash = Program.GoodHash(this.AviacompanyName);
        }

        public InfoGood(BaseInfo info) : base(info)
        {
            this.Hash = Program.GoodHash(this.AviacompanyName);
        }
    }

    public class InfoBad : BaseInfo
    {
        public UInt32 Hash;

        public InfoBad(int InfoId, int FlightNumber, string AviacompanyName, DateTime ArrivalDate, int PassengerNumber) : base(InfoId, FlightNumber, AviacompanyName, ArrivalDate, PassengerNumber)
        {
            this.Hash = Program.BadHash(this.AviacompanyName);
        }

        public InfoBad(BaseInfo info) : base(info)
        {
            this.Hash = Program.BadHash(this.AviacompanyName);
        }
    }
}
