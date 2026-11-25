using System.Collections.Generic;

namespace ZenStatesDebugTool
{
    /// <summary>Float indices of the PPT / TDC / EDC cells in a PM table. -1 means the cell is not known for that table.</summary>
    internal struct PowerLimitCells
    {
        public readonly int PptLimit, Ppt, TdcLimit, Tdc, EdcLimit, Edc;

        public PowerLimitCells(int pptLimit, int ppt, int tdcLimit, int tdc, int edcLimit, int edc)
        {
            PptLimit = pptLimit;
            Ppt = ppt;
            TdcLimit = tdcLimit;
            Tdc = tdc;
            EdcLimit = edcLimit;
            Edc = edc;
        }
    }

    /// <summary>
    /// Where the configured PPT (W), TDC (A) and EDC (A) limits sit in the PM table, per PM table version.
    /// </summary>
    internal static class PowerLimitMap
    {
        private static readonly Dictionary<uint, PowerLimitCells> Map = Build();

        public static bool TryGet(uint pmTableVersion, out PowerLimitCells cells)
        {
            return Map.TryGetValue(pmTableVersion, out cells);
        }

        private static void Add(Dictionary<uint, PowerLimitCells> map, PowerLimitCells cells, params uint[] versions)
        {
            foreach (uint version in versions)
                map[version] = cells;
        }

        private static Dictionary<uint, PowerLimitCells> Build()
        {
            var map = new Dictionary<uint, PowerLimitCells>();

            // PPT 0/1, TDC 2/3, EDC 11/-1 (limit/value)
            Add(map, new PowerLimitCells(0, 1, 2, 3, 11, -1),
                0x190001, 0x190101, 0x190201);

            // PPT 0/1, TDC 2/3, EDC 8/9 (limit/value)
            Add(map, new PowerLimitCells(0, 1, 2, 3, 8, 9),
                0x240001, 0x240002, 0x240003, 0x240004, 0x240101, 0x240102, 0x240103, 0x240104,
                0x240201, 0x240202, 0x240203, 0x240204, 0x240301, 0x240302, 0x240303, 0x240304,
                0x240401, 0x240402, 0x240403, 0x240404, 0x240501, 0x240502, 0x240503, 0x240504,
                0x240601, 0x240602, 0x240603, 0x240604, 0x240701, 0x240702, 0x240703, 0x240704,
                0x240801, 0x240802, 0x240803, 0x240901, 0x240902, 0x240903, 0x2D0000, 0x2D0002,
                0x2D0003, 0x2D0004, 0x2D0005, 0x2D0006, 0x2D0007, 0x2D0008, 0x2D0100, 0x2D0102,
                0x2D0103, 0x2D0104, 0x2D0105, 0x2D0106, 0x2D0107, 0x2D0108, 0x2D0200, 0x2D0202,
                0x2D0203, 0x2D0204, 0x2D0205, 0x2D0206, 0x2D0207, 0x2D0208, 0x2D0300, 0x2D0302,
                0x2D0303, 0x2D0304, 0x2D0305, 0x2D0306, 0x2D0307, 0x2D0308, 0x2D0400, 0x2D0402,
                0x2D0403, 0x2D0404, 0x2D0405, 0x2D0406, 0x2D0407, 0x2D0408, 0x2D0500, 0x2D0502,
                0x2D0503, 0x2D0504, 0x2D0505, 0x2D0506, 0x2D0507, 0x2D0508, 0x2D0600, 0x2D0602,
                0x2D0603, 0x2D0604, 0x2D0605, 0x2D0606, 0x2D0607, 0x2D0608, 0x2D0700, 0x2D0702,
                0x2D0703, 0x2D0704, 0x2D0705, 0x2D0706, 0x2D0707, 0x2D0708, 0x2D0800, 0x2D0802,
                0x2D0803, 0x2D0900, 0x2D0902, 0x2D0903, 0x380004, 0x380005, 0x380104, 0x380105,
                0x380204, 0x380205, 0x380304, 0x380305, 0x380404, 0x380405, 0x380504, 0x380505,
                0x380604, 0x380605, 0x380704, 0x380705, 0x380804, 0x380805, 0x380904, 0x380905,
                0x380A04, 0x380A05, 0x380B04, 0x380B05, 0x430000, 0x430001, 0x430701);

            // PPT 2/38, TDC 8/9, EDC 12/13 (limit/value)
            Add(map, new PowerLimitCells(2, 38, 8, 9, 12, 13),
                0x370000, 0x370001, 0x370002, 0x370003, 0x370005);

            // PPT 2/7, TDC 8/9, EDC 16/17 (limit/value)
            Add(map, new PowerLimitCells(2, 7, 8, 9, 16, 17),
                0x3F0000, 0x3F0001, 0x3F0002, 0x3F0003, 0x3F0103);

            // PPT 2/7, TDC 8/9, EDC 12/13 (limit/value)
            Add(map, new PowerLimitCells(2, 7, 8, 9, 12, 13),
                0x400001, 0x400002, 0x400003, 0x400004, 0x400005, 0x450001, 0x450002, 0x450003,
                0x450004, 0x450005, 0x4C0001, 0x4C0002, 0x4C0003, 0x4C0004, 0x4C0005, 0x4C0006,
                0x4C0007, 0x4C0008, 0x4C0009, 0x5A0001, 0x5A0002, 0x5D0001, 0x5D0002, 0x5D0003);

            // PPT 0/1, TDC 2/3, EDC -1/-1 (limit/value)
            Add(map, new PowerLimitCells(0, 1, 2, 3, -1, -1),
                0x470000, 0x470100, 0x470200, 0x470300, 0x470400);

            // PPT 0/1, TDC 2/3, EDC 75/76 (limit/value)
            Add(map, new PowerLimitCells(0, 1, 2, 3, 75, 76),
                0x470001, 0x470101, 0x470201, 0x470301, 0x470401);

            // PPT 0/1, TDC 2/3, EDC 78/79 (limit/value)
            Add(map, new PowerLimitCells(0, 1, 2, 3, 78, 79),
                0x470002, 0x470102, 0x470202, 0x470302, 0x470402, 0x5B0002, 0x5B0102, 0x5B0202,
                0x5B0302);

            // PPT 0/1, TDC 2/3, EDC 80/81 (limit/value)
            Add(map, new PowerLimitCells(0, 1, 2, 3, 80, 81),
                0x470003, 0x470103, 0x470203, 0x470303, 0x470403, 0x5B0003, 0x5B0103, 0x5B0203,
                0x5B0303);

            // PPT 0/1, TDC 2/3, EDC 84/85 (limit/value)
            Add(map, new PowerLimitCells(0, 1, 2, 3, 84, 85),
                0x470004, 0x470104, 0x470204, 0x470304, 0x470404, 0x5B0004, 0x5B0104, 0x5B0204,
                0x5B0304, 0x5B0404);

            // PPT 0/1, TDC 2/3, EDC 42/43 (limit/value)
            Add(map, new PowerLimitCells(0, 1, 2, 3, 42, 43),
                0x540000, 0x540001, 0x540100, 0x540101);

            // PPT 2/7, TDC 8/9, EDC 55/56 (limit/value)
            Add(map, new PowerLimitCells(2, 7, 8, 9, 55, 56),
                0x540002, 0x540003, 0x540102, 0x540103);

            // PPT 2/7, TDC 8/9, EDC 61/62 (limit/value)
            Add(map, new PowerLimitCells(2, 7, 8, 9, 61, 62),
                0x540004, 0x540005, 0x540104, 0x540105, 0x540107, 0x540207, 0x620100, 0x620200,
                0x621100);

            // PPT 2/7, TDC 8/9, EDC 76/77 (limit/value)
            Add(map, new PowerLimitCells(2, 7, 8, 9, 76, 77),
                0x540006, 0x540106);

            // PPT 2/7, TDC 8/9, EDC 62/63 (limit/value)
            Add(map, new PowerLimitCells(2, 7, 8, 9, 62, 63),
                0x540108, 0x540208, 0x620101, 0x620102, 0x620201, 0x620202, 0x621200);

            // PPT 0/1, TDC 2/3, EDC 81/82 (limit/value)
            Add(map, new PowerLimitCells(0, 1, 2, 3, 81, 82),
                0x5C0000, 0x5C0001, 0x5C0002, 0x5C0100, 0x5C0101, 0x5C0102, 0x5C0200, 0x5C0201,
                0x5C0202, 0x5C0300, 0x5C0301, 0x5C0302, 0x5C0400, 0x5C0401, 0x5C0402);

            // PPT 0/1, TDC 2/3, EDC 83/84 (limit/value)
            Add(map, new PowerLimitCells(0, 1, 2, 3, 83, 84),
                0x5C0003, 0x5C0103, 0x5C0203, 0x5C0303, 0x5C0403);

            // PPT 2/7, TDC 12/13, EDC 32/37 (limit/value)
            Add(map, new PowerLimitCells(2, 7, 12, 13, 32, 37),
                0x5D0004, 0x5D0005, 0x5D0006, 0x5D0007, 0x5D0008, 0x5D0009, 0x5D000A, 0x650001);

            // PPT 2/7, TDC 12/13, EDC 28/33 (limit/value)
            Add(map, new PowerLimitCells(2, 7, 12, 13, 28, 33),
                0x5D000B, 0x650007);

            // PPT 2/7, TDC 14/15, EDC 30/35 (limit/value)
            Add(map, new PowerLimitCells(2, 7, 14, 15, 30, 35),
                0x5D000C, 0x650008);

            // PPT 0/1, TDC 2/3, EDC 88/89 (limit/value)
            Add(map, new PowerLimitCells(0, 1, 2, 3, 88, 89),
                0x5E0200, 0x5E0400, 0x5E0600, 0x5E0800, 0x5E0A00, 0x5E0C00, 0x5E0E00, 0x5E1000);

            // PPT 0/1, TDC 2/3, EDC 98/99 (limit/value)
            Add(map, new PowerLimitCells(0, 1, 2, 3, 98, 99),
                0x5E0201, 0x5E0202, 0x5E0401, 0x5E0402, 0x5E0601, 0x5E0602, 0x5E0801, 0x5E0802,
                0x5E0A01, 0x5E0A02, 0x5E0C01, 0x5E0C02, 0x5E0E01, 0x5E0E02, 0x5E1001, 0x5E1002,
                0x630200, 0x630201, 0x630202, 0x630203, 0x630400, 0x630401, 0x630402, 0x630403,
                0x630600, 0x630601, 0x630602, 0x630603, 0x630701, 0x630702, 0x630703, 0x630800,
                0x630801, 0x630802, 0x630803, 0x630A00, 0x630A01, 0x630A02, 0x630A03, 0x630C00,
                0x630C01, 0x630C02, 0x630C03, 0x730201, 0x730202, 0x730401, 0x730402, 0x730601,
                0x730602, 0x730801, 0x730802, 0x730A01, 0x730A02, 0x730C01, 0x730C02, 0x730E01,
                0x730E02, 0x731001, 0x731002);

            // PPT 2/7, TDC 8/9, EDC 63/64 (limit/value)
            Add(map, new PowerLimitCells(2, 7, 8, 9, 63, 64),
                0x620103, 0x620104, 0x620105, 0x620106, 0x620203, 0x620204, 0x620205, 0x620206,
                0x621101, 0x621102, 0x621201, 0x621202);

            // PPT 2/7, TDC 12/13, EDC 32/34 (limit/value)
            Add(map, new PowerLimitCells(2, 7, 12, 13, 32, 34),
                0x640101, 0x640201);

            // PPT 2/7, TDC 12/13, EDC 34/40 (limit/value)
            Add(map, new PowerLimitCells(2, 7, 12, 13, 34, 40),
                0x640102, 0x640202);

            // PPT 2/7, TDC 12/13, EDC 36/42 (limit/value)
            Add(map, new PowerLimitCells(2, 7, 12, 13, 36, 42),
                0x640103, 0x640104, 0x640105, 0x640106, 0x640107, 0x640108, 0x640109, 0x64010A,
                0x64010B, 0x64010C, 0x640203, 0x640204, 0x640205, 0x640206, 0x640207, 0x640208,
                0x640209, 0x64020A, 0x64020B, 0x64020C);

            // PPT 2/7, TDC 14/15, EDC 38/44 (limit/value)
            Add(map, new PowerLimitCells(2, 7, 14, 15, 38, 44),
                0x64010D, 0x64010E, 0x64020D, 0x64020E);

            // PPT 2/7, TDC 12/13, EDC 30/35 (limit/value)
            Add(map, new PowerLimitCells(2, 7, 12, 13, 30, 35),
                0x650002, 0x650003, 0x650004, 0x650005, 0x650006);

            // PPT 2/7, TDC 14/15, EDC 34/40 (limit/value)
            Add(map, new PowerLimitCells(2, 7, 14, 15, 34, 40),
                0x650009);

            // PPT 2/7, TDC 14/15, EDC 30/36 (limit/value)
            Add(map, new PowerLimitCells(2, 7, 14, 15, 30, 36),
                0x65000A, 0x65000B, 0x65000C, 0x65000D, 0x65000E);

            // PPT 0/1, TDC 2/3, EDC 100/101 (limit/value)
            Add(map, new PowerLimitCells(0, 1, 2, 3, 100, 101),
                0x730203, 0x730403, 0x730603, 0x730803, 0x730A03, 0x730C03, 0x730E03, 0x731003);

            // PPT 0/1, TDC 2/3, EDC 114/115 (limit/value)
            Add(map, new PowerLimitCells(0, 1, 2, 3, 114, 115),
                0x730204, 0x730404, 0x730604, 0x730804, 0x730A04, 0x730C04, 0x730E04, 0x731004);

            return map;
        }
    }
}
