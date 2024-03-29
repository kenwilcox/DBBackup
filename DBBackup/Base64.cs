﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DBBackup
{
  public static class Base64
  {
    /// <summary>
    /// The method create a Base64 encoded string from a normal string.
    /// </summary>
    /// <param name="toEncode">The String containing the characters to encode.</param>
    /// <returns>The Base64 encoded string.</returns>
    public static string Encode(string toEncode)
    {
      byte[] toEncodeAsBytes = System.Text.Encoding.UTF8.GetBytes(toEncode);
      string returnValue = System.Convert.ToBase64String(toEncodeAsBytes);
      return returnValue;
    }

    /// <summary>
    /// The method to Decode your Base64 strings.
    /// </summary>
    /// <param name="encodedData">The String containing the characters to decode.</param>
    /// <returns>A String containing the results of decoding the specified sequence of bytes.</returns>
    public static string Decode(string encodedData)
    {
      byte[] encodedDataAsBytes = System.Convert.FromBase64String(encodedData);
      string returnValue = System.Text.Encoding.UTF8.GetString(encodedDataAsBytes);
      return returnValue;
    }

  }
}
