﻿using System;
using System.Collections.Generic;
using System.Xml.Serialization;

namespace Awine.Teach.DocumentService.TencentCos
{
    public class ListAllMyBucketsResult
    {
        public ListAllMyBucketsResult()
        {
            Buckets = new List<BucketResult>();
        }

        public BucketOwnerResult Owner { get; set; }

        public List<BucketResult> Buckets { get; set; }
    }

    public class BucketOwnerResult
    {
        [XmlElement("ID")]
        public string Id { get; set; }

        public string DisplayName { get; set; }
    }

    [XmlType("Bucket")]
    public class BucketResult
    {
        public string Name { get; set; }

        public string Location { get; set; }

        public string CreationDate { get; set; }

        public CosBucket GetBucket() { return new CosBucket(this); }

        public DateTimeOffset GetCreationDate() { return DateTimeOffset.Parse(CreationDate); }
    }
}
