Tool: FINDLINE

SetToolOrigin(): Cài đặt ROI chạy theo ToolOrigin.
	- INPUT:
		+ RTCRectangle ROI: ROI
		+ Tuple<Point, double> ToolOrigin: ToolOrigin
	- Chú ý: Mỗi lần ROI bị thay đổi (kích thước, vị trí) hoặc thay đổi Link Origin thì cần gọi lại hàm này.

Run(): Chạy tool FindLine
	-INPUT:
		+ Bitmap InputImage: Ảnh input.
		+ Tuple<Point, double> ToolOrigin: ToolOrigin (cần set lại trước khi Run())
		+ string EdgeTransition: Kiểu tìm cạnh
			. Light to Dark: Tìm cạnh khi chuyển từ sáng sang tối.
			. Dark to Light: Tìm cạnh khi chuyển từ tối sang sáng.
		+ string EdgeType: Loại cạnh
			. FirstEdge: Cạnh đầu tiên.
			. LastEdge: Cạnh cuối cùng.
		+ int EdgeDetectionThreshold: Ngưỡng chênh lệnh giá trị màu xám để phát hiện cạnh.
		+ int OutlierDistanceThreshold: Các điểm nằm ngoài khoảng này sẽ không được tính vào cạnh tìm được.
		+ int SamplingPercent: Tỉ lệ lấy mẫu hàng.
		+ double MaxPercentageOfOutliers: Tool sẽ trả ra Fail nếu tỉ lệ giữa các số lượng OutlierPoint với số lượng							OutputPointList lớn hơn giá trị này.
		+ int MinEdgePointNumber: Tool sẽ trả ra Fail nếu số lượng EdgePoints nhỏ hơn giá trị này.
		+ bool EnableGapLengthCheck: Bật kiểm tra GreatestGapLength so với GapLengthRange. Nếu GreatestGapLength nằm ngoài
					GapLengthRange thì tool sẽ fail.
		+ bool EnableLineLengthCheck: Bật filter lọc theo độ dài. Tool sẽ fail nếu độ dài cạnh tìm được nằm ngoài LineLengthTolerance
		+ bool EnableAngleRangeCheck: Bật filter lọc theo góc. Tool sẽ fail nếu góc của cạnh tìm được nằm ngoài LineAngleTolerance
		+ Tuple<double, double> GapLengthRange: Khoảng cho phép của GreatestGapLength.
		+ Tuple<double, double, double> LineLengthTolerance: Dung sai độ dài của bộ lọc filter. Có giá trị từ LineLengthTolerance.Item2 - LineLengthTolerance.Item1 đến LineLengthTolerance.Item2 + LineLengthTolerance.Item3.
		+ Tuple<double, double, double> LineAngleTolerance: Dung sai góc của bộ lọc filter. Có giá trị từ LineAngleTolerance.Item2 - LineAngleTolerance.Item1 đến LineAngleTolerance.Item2 + LineAngleTolerance.Item3.	
	-OUTPUT:
		+ List<Point> EdgePoints: Các điểm dùng để tạo đường tìm được.
		+ List<Point> OutlierPoints: Các điểm có khoảng cách đến đường tìm được lớn hơn giá trị OutlierDistanceThreshold
		+ List<Point> OutputPointList: Tất cả các điểm tìm được
		+ Tuple<Point, Point> LineSegment: Đường tìm được.
		+ double GreatestGapLength: Khoảng cách lớn nhất giữa 2 điểm EdgePoints liền kề nhau.
		+ double GreatestOutlierDistance: Khoảng cách lớn nhất từ điểm OutlierPoints đến đường tìm được.
		+ double PercentageOfOutliers: Tỉ lệ phần trăm các điểm OutlierPoints so với OutputPointList.
		+ double LineLengthActual: Chiều dài của cạnh tìm được.
		+ double LineAngleActual: Góc của cạnh tìm được so với trục Ox.
		+ bool Passed: nếu true : passed; nếu fasle: failed.
		+ string ErrMessage: message lỗi.
		+ Bitmap OutputImage: Ảnh output.
	-Chú ý : Cần gọi hàm SetToolOrigin() trước khi chạy hàm Run().

