Public Class CoinDirectory
    Inherits Dictionary(Of Int32, Coin)
    Private CoinsInView As New HashSet(Of Coin)



    Public Sub AddorUpdate(croppedImage As CroppedImage, ByRef coinIsDup As Boolean, ByRef coinID As Int32)
        coinID = GetCoinID(croppedImage)

        If coinID = 0 Then
            coinIsDup = False
            Dim coin As New Coin(croppedImage)
            coinID = coin.coinID
            Me.Add(coin.coinID, coin)
            CoinsInView.Add(coin)
        Else
            coinIsDup = True
            Me(coinID).CoinCroppedImages.Add(croppedImage)
        End If
    End Sub

    Private Function GetCoinID(croppedImage As CroppedImage) As Int32
        ' updateCoinsInView()
        For Each coin In CoinsInView
            Dim span As TimeSpan = croppedImage.CaptureTime - coin.CaptureTime
            Dim msDiff As Double = span.TotalMilliseconds
            Dim pixelDiff As Double = msDiff * CoinSpeedPixelsPerMs
            Dim projectedHorizontalCenter As Double = coin.HorizontalCenter + pixelDiff
            Dim projectedHorizontalCenterDiff As Double = projectedHorizontalCenter - croppedImage.HorizontalCenter
            If Math.Abs(projectedHorizontalCenterDiff) < CoinRadiusOnFullImage Then
                Dim ActualCoinSpeedPixelsPerMs As Double = (croppedImage.HorizontalCenter - coin.HorizontalCenter) / msDiff
                Dim NewCoinSpeedPixelsPerMs As Double = CoinSpeedPixelsPerMs * 0.95 + ActualCoinSpeedPixelsPerMs * 0.05
                Console.WriteLine()
                Console.WriteLine("CroppedImageID: " & croppedImage.CroppedImageID.ToString)
                Console.WriteLine("CoinID: " & coin.coinID.ToString)
                Console.WriteLine("Span: " & span.ToString)
                Console.WriteLine("msDiff: " & msDiff.ToString)
                Console.WriteLine("pixelDiff: " & pixelDiff.ToString)
                Console.WriteLine("projectedHorizontalCenter: " & projectedHorizontalCenter.ToString)
                Console.WriteLine("projectedHorizontalCenterDiff: " & projectedHorizontalCenterDiff.ToString)
                Console.WriteLine("ActualCoinSpeedPixelsPerMs: " & ActualCoinSpeedPixelsPerMs.ToString)
                Console.WriteLine("NewCoinSpeedPixelsPerMs: " & NewCoinSpeedPixelsPerMs.ToString)
                Return coin.coinID
            End If
        Next
        Return 0
    End Function

    Private Sub updateCoinsInView()
        Dim newCoinsInView As New HashSet(Of Coin)
        For Each coin In CoinsInView
            Dim span As TimeSpan = Now() - coin.CaptureTime
            Dim msDiff As Int32 = span.TotalMilliseconds
            If msDiff > MsCoinsAreInView Then
                newCoinsInView.Add(coin)
            End If
        Next
        CoinsInView = newCoinsInView
    End Sub
End Class
