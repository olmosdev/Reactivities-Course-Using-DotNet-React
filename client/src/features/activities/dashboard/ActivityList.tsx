import Box from "@mui/material/Box";
import ActivityCard from "./ActivityCard";

type ActivityListProps = {
  activities: Activity[];
  selectActivity: (id: string) => void;
};

export default function ActivityList({
  activities,
  selectActivity,
}: ActivityListProps) {
  return (
    <Box sx={{ display: "flex", flexDirection: "column", gap: 3 }}>
      {activities.map((activity) => (
        <ActivityCard
          key={activity.id}
          activity={activity}
          selectActivity={selectActivity}
        />
      ))}
    </Box>
  );
}
