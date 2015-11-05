create table Angles4 as
select images.imageID as ImageID
, avg((results.LabelID-ModelID) * Score) as Delta 
,images.angle + (avg((results.LabelID-ModelID) * Score)) as NewAngle
, images.angle as OldAngle
from results 
inner join images
on results.imageID = images.imageID
where abs(modelID-results.LabelID) < 3 
and images.angle is not null
group by  images.imageID, images.angle
order by 2;