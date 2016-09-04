﻿/*
 * QUANTCONNECT.COM - Democratizing Finance, Empowering Individuals.
 * Lean Algorithmic Trading Engine v2.0. Copyright 2014 QuantConnect Corporation.
 * 
 * Licensed under the Apache License, Version 2.0 (the "License"); 
 * you may not use this file except in compliance with the License.
 * You may obtain a copy of the License at http://www.apache.org/licenses/LICENSE-2.0
 * 
 * Unless required by applicable law or agreed to in writing, software
 * distributed under the License is distributed on an "AS IS" BASIS,
 * WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
 * See the License for the specific language governing permissions and
 * limitations under the License.
 *
*/

using System;

namespace QuantConnect.Data.Market
{
    /// <summary>
    /// Dividend event from a security
    /// </summary>
    public class Dividend : BaseData
    {
        /// <summary>
        /// Initializes a new instance of the Dividend class
        /// </summary>
        public Dividend()
        {
            DataType = MarketDataType.Auxiliary;
        }

        /// <summary>
        /// Initializes a new instance of the Dividend class
        /// </summary>
        /// <param name="symbol">The symbol</param>
        /// <param name="date">The date</param>
        /// <param name="close">The close</param>
        /// <param name="priceFactorRatio">The ratio of the price factors, pf_i/pf_i+1</param>
        public Dividend(Symbol symbol, DateTime date, decimal close, decimal priceFactorRatio)
            : this()
        {
            Symbol = symbol;
            Time = date;
            Distribution = close - (close * priceFactorRatio);
        }

        /// <summary>
        /// Initializes a new instance of the Dividend class
        /// </summary>
        /// <param name="symbol">The symbol</param>
        /// <param name="date">The date</param>
        /// <param name="distribution">The dividend amount</param>
        public Dividend(Symbol symbol, DateTime date, decimal distribution)
            : this()
        {
            Symbol = symbol;
            Time = date;
            Distribution = distribution;
        }

        /// <summary>
        /// Gets the dividend payment
        /// </summary>
        public decimal Distribution
        {
            get { return Value; } 
            set { Value = Math.Round(value, 2); }
        }

        /// <summary>
        /// Reader converts each line of the data source into BaseData objects. Each data type creates its own factory method, and returns a new instance of the object 
        /// each time it is called. 
        /// </summary>
        /// <param name="config">Subscription data config setup object</param>
        /// <param name="line">Line of the source document</param>
        /// <param name="date">Date of the requested data</param>
        /// <param name="isLiveMode">true if we're in live mode, false for backtesting mode</param>
        /// <returns>Instance of the T:BaseData object generated by this line of the CSV</returns>
        public override BaseData Reader(SubscriptionDataConfig config, string line, DateTime date, bool isLiveMode)
        {
            // this is implemented in the SubscriptionDataReader.CheckForDividend
            throw new NotImplementedException("This method is not supposed to be called on the Dividend type.");
        }

        /// <summary>
        /// Return the URL string source of the file. This will be converted to a stream 
        /// </summary>
        /// <param name="config">Configuration object</param>
        /// <param name="date">Date of this source file</param>
        /// <param name="isLiveMode">true if we're in live mode, false for backtesting mode</param>
        /// <returns>String URL of source file.</returns>
        public override SubscriptionDataSource GetSource(SubscriptionDataConfig config, DateTime date, bool isLiveMode)
        {
            // this data is derived from map files and factor files in backtesting
            throw new NotImplementedException("This method is not supposed to be called on the Dividend type.");
        }

        /// <summary>
        /// Return a new instance clone of this object, used in fill forward
        /// </summary>
        /// <remarks>
        /// This base implementation uses reflection to copy all public fields and properties
        /// </remarks>
        /// <returns>A clone of the current object</returns>
        public override BaseData Clone()
        {
            return new Dividend(Symbol, Time, Distribution);
        }
    }
}
