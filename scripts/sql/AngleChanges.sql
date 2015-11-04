create table Angles1 as
select images.imageID as ImageID
, avg((results.LabelID-ModelID) * Score) as Delta 
,images.angle - (avg((results.LabelID-ModelID) * Score)*2) as NewAngle
, images.angle as OldAngle
from results 
inner join images
on results.imageID = images.imageID
where abs(modelID-results.LabelID) < 3 
group by  images.imageID, images.angle
order by 2;